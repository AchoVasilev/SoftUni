namespace SoftJail.DataProcessor.ExportDto
{
    public class JsonPrisonerExportModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CellNumber { get; set; }

        public JsonOfficerExportModel[] Officers { get; set; }

        public decimal TotalOfficerSalary { get; set; }
    }
}
