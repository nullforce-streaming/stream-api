﻿using System;

namespace StreamApis.Quotes
{
    public class AddQuoteViewModel
    {
        public string Category { get; set; }
        public string Quote { get; set; }
        public string Who { get; set; }
        public DateTimeOffset When { get; set; }
    }
}
