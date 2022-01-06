namespace Cinema.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Cinema.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            JsonMovieExportModel[] movieModels = context.Movies
                .ToArray()
                .Where(x => x.Rating >= rating && x.Projections.Any(z => z.Tickets.Count > 0))
                .Select(x => new JsonMovieExportModel
                {
                    MovieName = x.Title,
                    Rating = x.Rating.ToString("F2"),
                    TotalIncomes = x.Projections.Sum(y => y.Tickets.Sum(z => z.Price)).ToString("F2"),
                    Customers = x.Projections.SelectMany(x => x.Tickets)
                                             .Select(y => new JsonCustomerExportModel
                                             {
                                                 FirstName = y.Customer.FirstName,
                                                 LastName = y.Customer.LastName,
                                                 Balance = y.Customer.Balance.ToString("F2")
                                             })
                                             .OrderByDescending(y => y.Balance)
                                             .ThenBy(y => y.FirstName)
                                             .ThenBy(y => y.LastName)
                                             .ToArray()
                })
                .OrderByDescending(x => x.Rating)
                .ThenByDescending(x => x.TotalIncomes)
                .Take(10)
                .ToArray();

            return JsonConvert.SerializeObject(movieModels, Formatting.Indented);
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            string root = "Customers";
            StringBuilder output = new StringBuilder();
            XmlSerializer serialzier = new XmlSerializer(typeof(XmlCustomerExportModel[]), new XmlRootAttribute(root));
            XmlCustomerExportModel[] customerModels = context.Customers
                .ToArray()
                .Where(x => x.Age >= age)
                .Select(x => new XmlCustomerExportModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SpentMoney = decimal.Parse(x.Tickets.Sum(x => x.Price).ToString("F2")),
                    SpentTime = new TimeSpan(x.Tickets.Sum(y => y.Projection.Movie.Duration.Ticks)).ToString(@"hh\:mm\:ss")
                })
                .OrderByDescending(x => x.SpentMoney)
                .Take(10)
                .ToArray();

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            using TextWriter writer = new StringWriter(output);
            serialzier.Serialize(writer, customerModels, ns);

            return output.ToString().TrimEnd();
        }
    }
}