using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Projection")]
    public class XmlProjectionImportModel
    {
        [XmlElement("MovieId")]
        public int MovieId { get; set; }

        [XmlElement("DateTime")]
        [Required]
        public string DateTime { get; set; }
    }
}
