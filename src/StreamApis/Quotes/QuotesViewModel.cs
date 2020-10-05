using System;

namespace Nullforce.StreamApis.Quotes
{
    public class QuotesViewModel
    {
        public class QuoteItemViewModel
        {
            public int Id { get; set; }
            public string Category { get; set; }
            public string Quote { get; set; }
            public string Who { get; set; }
            public DateTimeOffset When { get; set; }
        }

        public QuoteItemViewModel[] Quotes { get; set; }
    }
}
