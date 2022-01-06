using Newtonsoft.Json;

namespace ProductShop.DataTransferObjects
{
    public class UsersWithSoldProductsDto
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("soldProducts")]
        public UsersSoldProductsDto[] SoldProducts { get; set; }
    }
}
