namespace BookShop.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            AuthorJsonExportModel[] authorsJson = context.Authors
                .Select(x => new AuthorJsonExportModel
                {
                    AuthorName = x.FirstName + ' ' + x.LastName,
                    Books = x.AuthorsBooks
                    .OrderByDescending(y => y.Book.Price)
                    .Select(y => new BooksJsonExportModel
                    {
                        BookName = y.Book.Name,
                        BookPrice = y.Book.Price.ToString("F2")
                    })
                    .ToArray()
                })
                .ToArray()
                .OrderByDescending(x => x.Books.Length)
                .ThenBy(x => x.AuthorName)
                .ToArray();

            string output = JsonConvert.SerializeObject(authorsJson, Formatting.Indented);

            return output;
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            string root = "Books";

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BooksXmlExportModel[]), new XmlRootAttribute(root));

            BooksXmlExportModel[] booksXml = context.Books
                .Where(x => x.PublishedOn < date && x.Genre == Genre.Science)
                .OrderByDescending(x => x.Pages)
                .ThenByDescending(x => x.PublishedOn)
                .ToArray()
                .Select(x => new BooksXmlExportModel
                {
                    Name = x.Name,
                    Pages = x.Pages,
                    Date = x.PublishedOn.ToString("d", CultureInfo.InvariantCulture)
                })
                .Take(10)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            using (TextWriter writer = new StringWriter(sb))
            {
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");

                xmlSerializer.Serialize(writer, booksXml, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}