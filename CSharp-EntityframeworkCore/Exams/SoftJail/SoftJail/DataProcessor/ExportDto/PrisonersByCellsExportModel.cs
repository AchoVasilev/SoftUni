namespace SoftJail.DataProcessor.ExportDto
{
    public class PrisonersByCellsExportModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CellNumber { get; set; }

        public OfficerExportModel[] Officers { get; set; }

        public decimal TotalOfficerSalary { get; set; }
    }
}
