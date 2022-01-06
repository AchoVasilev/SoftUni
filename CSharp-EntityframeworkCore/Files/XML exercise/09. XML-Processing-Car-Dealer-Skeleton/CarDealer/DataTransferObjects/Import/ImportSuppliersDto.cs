using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.Import
{
    [XmlType("Supplier")]
    public class ImportSuppliersDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("isImporter")]
        public bool IsImporter { get; set; }
    }
}
