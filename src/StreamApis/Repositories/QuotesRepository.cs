using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StreamApis.Models;

namespace StreamApis.Repositories
{
    public class QuotesRepository : IQuotesRepository
    {
        private static readonly string[] Categories = new[]
        {
            "streamer",
            "ponies",
            "games",
            "facts",
        };

        private static readonly string[] Whos = new[]
        {
            "nullforce",
            "Albert Einstein",
            "fluttershy",
        };

        private static readonly string[] Quotes = new[]
        {
            "And I'm dead!",
            "E = mc^2",
            "I want to be a tree.",
        };

        public async Task<List<string>> GetCategories(string tenant)
        {
            return Categories.ToList();
        }

        public async Task<List<Quote>> GetQuotes(string tenant)
        {
            var rng = new Random();

            return Enumerable.Range(1, 5)
                .Select(index => new Quote
                {
                    Id = index,
                    Tenant = tenant,
                    Category = Categories[rng.Next(Categories.Length)],
                    Who = Whos[rng.Next(Whos.Length)],
                    When = DateTime.UtcNow.AddDays(-rng.Next(1, 20)),
                    QuoteString = Quotes[rng.Next(Quotes.Length)],
                }).ToList();
        }

        public async Task<List<Quote>> GetQuotes(string tenant, string category)
        {
            var rng = new Random();

            return Enumerable.Range(1, 5)
                .Select(index => new Quote
                {
                    Id = index,
                    Tenant = tenant,
                    Category = category,
                    Who = Whos[rng.Next(Whos.Length)],
                    When = DateTime.UtcNow.AddDays(-rng.Next(1, 20)),
                    QuoteString = Quotes[rng.Next(Quotes.Length)],
                }).ToList();
        }
    }
}
