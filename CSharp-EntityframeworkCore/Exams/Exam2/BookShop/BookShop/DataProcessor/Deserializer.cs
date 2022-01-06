namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using BookShop.Data.Models;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            string root = "Books";
            StringBuilder output = new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlBookImportModel[]), new XmlRootAttribute(root));
            List<Book> books = new List<Book>();

            using (TextReader reader = new StringReader(xmlString))
            {
                XmlBookImportModel[] bookModels = xmlSerializer.Deserialize(reader) as XmlBookImportModel[];

                foreach (XmlBookImportModel bookModel in bookModels)
                {
                    if (!IsValid(bookModel))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isValidDate = DateTime.TryParseExact(bookModel.PublishedOn, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publishedOn);

                    if (!isValidDate)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    Book book = new Book
                    {
                        Name = bookModel.Name,
                        Genre = Enum.Parse<Genre>(bookModel.Genre),
                        Price =  bookModel.Price,
                        Pages = bookModel.Pages,
                        PublishedOn = publishedOn
                    };

                    books.Add(book);
                    output.AppendLine(string.Format(SuccessfullyImportedBook, bookModel.Name, bookModel.Price));
                }
            };

            context.Books.AddRange(books);
            context.SaveChanges();

            return output.ToString().TrimEnd();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            StringBuilder output = new StringBuilder();
            JsonAuthorImportModel[] authorModels = JsonConvert.DeserializeObject<JsonAuthorImportModel[]>(jsonString);
            List<Author> authors = new List<Author>();

            foreach (JsonAuthorImportModel authorModel in authorModels)
            {
                if (!IsValid(authorModel))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                if (authors.Any(x => x.Email == authorModel.Email))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                Author author = new Author
                {
                    FirstName = authorModel.FirstName,
                    LastName = authorModel.LastName,
                    Phone = authorModel.Phone,
                    Email = authorModel.Email
                };

                foreach (var bookModel in authorModel.Books)
                {
                    if (!bookModel.Id.HasValue)
                    {
                        continue;
                    }

                    Book book = context.Books.FirstOrDefault(x => x.Id == bookModel.Id);

                    if (book == null)
                    {
                        continue;
                    }

                    author.AuthorsBooks.Add(new AuthorBook
                    {
                        Author = author,
                        Book = book
                    });
                }

                if (author.AuthorsBooks.Count == 0)
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                authors.Add(author);
                output.AppendLine(string.Format(SuccessfullyImportedAuthor, author.FirstName + ' ' + author.LastName, author.AuthorsBooks.Count));
            }

            context.Authors.AddRange(authors);
            context.SaveChanges();

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