using Newtonsoft.Json;

namespace BookShop.DataProcessor.ImportDto
{
    public class BooksJsonImportModel
    {
        [JsonProperty("Id")]
        public int? BookId { get; set; }
    }
}
