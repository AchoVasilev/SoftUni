using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.Export
{
    [XmlType("car")]
    public class ExportCarsDistanceDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("travelled-distance")]
        public long TravelledDistance { get; set; }
    }
}
