namespace BookShop.DataProcessor.ExportDto
{
    public class JsonAuthorExportModel
    {
        public string AuthorName { get; set; }

        public JsonBookExportModel[] Books { get; set; }
    }
}
