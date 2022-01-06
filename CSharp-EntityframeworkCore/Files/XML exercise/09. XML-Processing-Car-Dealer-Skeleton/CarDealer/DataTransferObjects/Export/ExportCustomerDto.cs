using System.Xml.Serialization;

namespace CarDealer.DataTransferObjects.Export
{
    [XmlType("customer")]
    public class ExportCustomerDto
    {
        [XmlAttribute("fullName")]
        public string FullName { get; set; }

        [XmlAttribute("bought-cars")]
        public int BoughtCars { get; set; }

        [XmlAttribute("spent-money")]
        public decimal SpentMoney { get; set; }
    }
}
