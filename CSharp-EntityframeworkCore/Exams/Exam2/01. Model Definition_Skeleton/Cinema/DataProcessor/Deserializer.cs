namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Cinema.Data.Models;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2}!";

        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";

        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            StringBuilder output = new StringBuilder();
            JsonMovieImportModel[] movieModels = JsonConvert.DeserializeObject<JsonMovieImportModel[]>(jsonString);

            foreach (var movieModel in movieModels)
            {
                if (!IsValid(movieModel))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                if (context.Movies.Any(x => x.Title == movieModel.Title))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                Movie movie = new Movie
                {
                    Title = movieModel.Title,
                    Genre = movieModel.Genre.Value,
                    Duration = movieModel.Duration.Value,
                    Rating = movieModel.Rating,
                    Director = movieModel.Director
                };

                context.Movies.Add(movie);
                context.SaveChanges();
                output.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre, movie.Rating.ToString("F2")));
            }

            return output.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            StringBuilder output = new StringBuilder();
            string root = "Projections";
            XmlSerializer serializer = new XmlSerializer(typeof(XmlProjectionImportModel[]), new XmlRootAttribute(root));

            using TextReader reader = new StringReader(xmlString);
            XmlProjectionImportModel[] projectModels = serializer.Deserialize(reader) as XmlProjectionImportModel[];

            foreach (var projectModel in projectModels)
            {
                if (!IsValid(projectModel))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                //if (!context.Movies.Any(x => x.Id == projectModel.MovieId))
                //{
                //    output.AppendLine(ErrorMessage);
                //    continue;
                //}

                bool isValidDate = DateTime.TryParseExact(projectModel.DateTime, "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);

                if (!isValidDate)
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                List<int> movieIds = context.Movies.Select(x => x.Id).ToList();

                if (!movieIds.Contains(projectModel.MovieId))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                Projection projection = new Projection
                {
                    MovieId = projectModel.MovieId,
                    DateTime = date
                };

                string movieTitle = context.Movies
                    .Where(x => x.Id == projection.MovieId)
                    .Select(x => x.Title)
                    .FirstOrDefault();

                context.Projections.Add(projection);
                output.AppendLine(string.Format(SuccessfulImportProjection, movieTitle, date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
            }

            context.SaveChanges();
            return output.ToString().TrimEnd();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            string root = "Customers";
            StringBuilder output = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(XmlCustomerImportModel[]), new XmlRootAttribute(root));

            using TextReader reader = new StringReader(xmlString);
            XmlCustomerImportModel[] customerModels = serializer.Deserialize(reader) as XmlCustomerImportModel[];

            foreach (var customerModel in customerModels)
            {
                if (!IsValid(customerModel))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                Customer customer = new Customer
                {
                    FirstName = customerModel.FirstName,
                    LastName = customerModel.LastName,
                    Age = customerModel.Age,
                    Balance = customerModel.Balance
                };

                foreach (var customerTicket in customerModel.Tickets)
                {
                    if (!IsValid(customerTicket))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (!context.Projections.Any(x => x.Id == customerTicket.ProjectionId))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    List<int> projectionIds = context.Projections.Select(x => x.Id).ToList();
                    if (!projectionIds.Contains(customerTicket.ProjectionId) && !projectionIds.Any(x => x == customerTicket.ProjectionId))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    var projection = context.Projections
                        .FirstOrDefault(x => x.Id == customerTicket.ProjectionId);

                    if (projection == null)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    Ticket ticket = new Ticket
                    {
                        Customer = customer,
                        ProjectionId = customerTicket.ProjectionId,
                        Price = customerTicket.Price
                    };

                    customer.Tickets.Add(ticket);
                }

                context.Customers.Add(customer);
                context.SaveChanges();
                output.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName, customer.Tickets.Count));
            }

            return output.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}