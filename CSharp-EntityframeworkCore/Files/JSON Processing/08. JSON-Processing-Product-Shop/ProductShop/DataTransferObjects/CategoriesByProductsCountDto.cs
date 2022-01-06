using Newtonsoft.Json;

namespace ProductShop.DataTransferObjects
{
    public class CategoriesByProductsCountDto
    {
        [JsonProperty("category")]
        public string Name { get; set; }

        [JsonProperty("productsCount")]
        public int ProductCount { get; set; }

        [JsonProperty("averagePrice")]
        public string AveragePrice { get; set; }

        [JsonProperty("totalRevenue")]
        public string TotalRevenue { get; set; }
    }
}
