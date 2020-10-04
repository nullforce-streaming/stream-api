using System;

namespace Nullforce.StreamApis.Quotes
{
    public class QuoteViewModel
    {
        public string Category { get; set; }
        public string Quote { get; set; }
        public string Who { get; set; }
        public DateTimeOffset When { get; set; }
    }
}
