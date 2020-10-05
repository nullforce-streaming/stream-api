using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace StreamApis.Cosmos
{
    public class QuotesDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tenant")]
        public string Tenant { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("quotes")]
        public List<Quote> Quotes { get; set; }

        [JsonProperty("_etag")]
        public string ETag { get; set; }

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
}
