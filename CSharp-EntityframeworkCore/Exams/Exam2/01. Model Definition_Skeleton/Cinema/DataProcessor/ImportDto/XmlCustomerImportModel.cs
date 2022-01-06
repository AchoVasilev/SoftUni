using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Customer")]
    public class XmlCustomerImportModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        [XmlElement("FirstName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [XmlElement("LastName")]
        public string LastName { get; set; }

        [Range(12, 110)]
        [XmlElement("Age")]
        public int Age { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        [XmlElement("Balance")]
        public decimal Balance { get; set; }

        [XmlArray("Tickets")]
        public XmlTicketImportModel[] Tickets { get; set; }
    }
}
