namespace BookShop.DataProcessor.ExportDto
{
    public class AuthorJsonExportModel
    {
        public string AuthorName { get; set; }

        public BooksJsonExportModel[] Books { get; set; }
    }
}
