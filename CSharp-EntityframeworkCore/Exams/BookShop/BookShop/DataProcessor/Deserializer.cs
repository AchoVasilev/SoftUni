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
            StringBuilder sb = new StringBuilder();

            string root = "Books";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BooksXmlImportModel[]), new XmlRootAttribute(root));
            HashSet<Book> books = new HashSet<Book>();

            using (TextReader reader = new StringReader(xmlString))
            {
                BooksXmlImportModel[] booksXml = xmlSerializer.Deserialize(reader) as BooksXmlImportModel[];

                foreach (var bookXml in booksXml)
                {
                    if (!IsValid(bookXml))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isDateValid = DateTime.TryParseExact(bookXml.PublishedOn, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publishedOn);

                    if (!isDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Book book = new Book
                    {
                        Name = bookXml.Name,
                        Genre = (Genre)bookXml.Genre,
                        Price = bookXml.Price,
                        Pages = bookXml.Pages,
                        PublishedOn = publishedOn
                    };

                    books.Add(book);
                    sb.AppendLine(string.Format(SuccessfullyImportedBook, book.Name, book.Price));
                }

                context.Books.AddRange(books);
                context.SaveChanges();

                return sb.ToString().TrimEnd();
            }
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            AuthorsJsonImportModel[] authorsJson = JsonConvert.DeserializeObject<AuthorsJsonImportModel[]>(jsonString);
            List<Author> authors = new List<Author>();

            foreach (var authorJson in authorsJson)
            {
                if (!IsValid(authorJson))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (authors.Any(a => a.Email == authorJson.Email))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Author author = new Author
                {
                    FirstName = authorJson.FirstName,
                    LastName = authorJson.LastName,
                    Email = authorJson.Email,
                    Phone = authorJson.Phone
                };

                foreach (var bookModel in authorJson.Books)
                {
                    if (!bookModel.BookId.HasValue)
                    {
                        continue;
                    }

                    Book book = context.Books.FirstOrDefault(b => b.Id == bookModel.BookId);

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
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                authors.Add(author);
                sb.AppendLine(string.Format(SuccessfullyImportedAuthor, author.FirstName + ' ' + author.LastName, author.AuthorsBooks.Count));
            }

            context.Authors.AddRange(authors);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}