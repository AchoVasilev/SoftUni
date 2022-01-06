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
            JsonAuthorExportModel[] authorModels = context.Authors
                .Select(x => new JsonAuthorExportModel
                {
                    AuthorName = x.FirstName + ' ' + x.LastName,
                    Books = x.AuthorsBooks
                    .OrderByDescending(y => y.Book.Price)
                    .Select(y => new JsonBookExportModel
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

            return JsonConvert.SerializeObject(authorModels, Formatting.Indented);
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            string root = "Books";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlBookExportModel[]), new XmlRootAttribute(root));

            XmlBookExportModel[] bookModels = context.Books
                .ToArray()
                .Where(x => x.Genre == Genre.Science && x.PublishedOn < date)
                .OrderByDescending(x => x.Pages)
                .ThenByDescending(x => x.PublishedOn)
                .Select(x => new XmlBookExportModel
                {
                    Name = x.Name,
                    Pages = x.Pages,
                    Date = x.PublishedOn.ToString("d", CultureInfo.InvariantCulture)
                })
                .Take(10)
                .ToArray();

            StringBuilder output = new StringBuilder();
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (TextWriter writer = new StringWriter(output))
            {
                xmlSerializer.Serialize(writer, bookModels, namespaces);
            };

            return output.ToString().TrimEnd();
        }
    }
}