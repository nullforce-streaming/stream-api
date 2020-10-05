using System;

namespace StreamApis.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string Tenant { get; set; }
        public string Category { get; set; }
        public string QuoteString { get; set; }
        public string Who { get; set; }
        public DateTimeOffset When { get; set; }
    }
}
