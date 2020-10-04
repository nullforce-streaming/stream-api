using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StreamApis.Models;
using Microsoft.Azure.Cosmos;

namespace StreamApis.Repositories
{
    public class QuotesRepository : IQuotesRepository
    {
        private readonly CosmosClient _client;
        private readonly Container _container;

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

        public QuotesRepository(CosmosClient cosmosClient)
        {
            _client = cosmosClient;
            _container = _client.GetContainer("streamapi", "quotes");
        }

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
                    Id = index.ToString(),
                    Tenant = tenant,
                    Category = Categories[rng.Next(Categories.Length)],
                    Who = Whos[rng.Next(Whos.Length)],
                    When = DateTime.UtcNow.AddDays(-rng.Next(1, 20)),
                    QuoteString = Quotes[rng.Next(Quotes.Length)],
                }).ToList();
        }

        public async Task<List<Quote>> GetQuotes(string tenant, string category)
        {
            var query = new QueryDefinition("select * from Quotes q where q.category = @category")
                .WithParameter("@category", category);
            var resultIterator = _container.GetItemQueryIterator<QuoteContainer>(query);
            var results = new List<QuoteContainer>();

            while (resultIterator.HasMoreResults)
            {
                var response = await resultIterator.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            var quotes = new List<Quote>();

            foreach (var qc in results)
            {
                quotes.AddRange(
                    qc.Quotes.Select(q => new Quote
                    {
                        Id = q.Id,
                        Tenant = qc.Tenant,
                        Category = qc.Category,
                        Who = q.Who,
                        When = q.When,
                        QuoteString = q.QuoteString,
                    }));
            }

            return quotes;
        }
    }
}
