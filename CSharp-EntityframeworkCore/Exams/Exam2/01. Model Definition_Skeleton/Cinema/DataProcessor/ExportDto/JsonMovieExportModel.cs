namespace Cinema.DataProcessor.ExportDto
{
    public class JsonMovieExportModel
    {
        public string MovieName { get; set; }

        public string Rating { get; set; }

        public string TotalIncomes { get; set; }

        public JsonCustomerExportModel[] Customers { get; set; }
    }
}
