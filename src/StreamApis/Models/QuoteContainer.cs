using Newtonsoft.Json;

namespace StreamApis.Models
{
    public class QuoteContainer
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tenant")]
        public string Tenant { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("quotes")]
        public Quote[] Quotes { get; set; }
    }
}
