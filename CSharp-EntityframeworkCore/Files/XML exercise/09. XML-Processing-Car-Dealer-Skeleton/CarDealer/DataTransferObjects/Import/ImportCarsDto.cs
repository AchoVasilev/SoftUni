using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.Import
{
    [XmlType("Car")]
    public class ImportCarsDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public long TravelledDistance { get; set; }

        [XmlArray("parts")]
        public Parts[] Parts { get; set; }
    }

    [XmlType("partId")]
    public class Parts
    {
        [XmlAttribute("id")]
        public int PartId { get; set; }
    }
}
