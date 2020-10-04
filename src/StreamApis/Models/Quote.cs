using Newtonsoft.Json;
using System;

namespace StreamApis.Models
{
    public class Quote
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("tenant")]
        public string Tenant { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("quote")]
        public string QuoteString { get; set; }

        [JsonProperty("who")]
        public string Who { get; set; }

        [JsonProperty("when")]
        public DateTimeOffset When { get; set; }
    }
}
