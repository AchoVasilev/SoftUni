using Newtonsoft.Json;

namespace BookShop.DataProcessor.ImportDto
{
    public class JsonBooksImportModel
    {
        [JsonProperty("Id")]
        public int? Id { get; set; }
    }
}