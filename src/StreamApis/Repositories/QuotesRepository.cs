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

        public QuotesRepository(CosmosClient cosmosClient)
        {
            _client = cosmosClient;
            _container = _client.GetContainer("streamapi", "quotes");
        }

        public async Task<List<string>> GetCategories(string tenant)
        {
            var query = new QueryDefinition("select distinct q.category from Quotes q where q.tenant = @tenant")
                .WithParameter("@tenant", tenant);
            var resultIterator = _container.GetItemQueryIterator<QuoteContainer>(query);
            var results = new List<QuoteContainer>();

            while (resultIterator.HasMoreResults)
            {
                var response = await resultIterator.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results.Select(qc => qc.Category).ToList();
        }

        public async Task<List<Quote>> GetQuotes(string tenant)
        {
            var query = new QueryDefinition("select * from Quotes q where q.tenant = @tenant")
                .WithParameter("@tenant", tenant);
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

        public async Task<List<Quote>> GetQuotes(string tenant, string category)
        {
            var query = new QueryDefinition("select * from Quotes q where q.tenant = @tenant and q.category = @category")
                .WithParameter("@tenant", tenant)
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
