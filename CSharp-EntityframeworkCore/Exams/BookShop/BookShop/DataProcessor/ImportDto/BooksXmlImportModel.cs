using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

using BookShop.Data.Models.Enums;

namespace BookShop.DataProcessor.ImportDto
{
    [XmlType("Book")]
    public class BooksXmlImportModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(1, 3)]
        public int Genre { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Range(50, 5000)]
        public int Pages { get; set; }

        public string PublishedOn { get; set; }
    }
}
