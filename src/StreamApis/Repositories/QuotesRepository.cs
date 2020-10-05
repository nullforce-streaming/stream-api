using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StreamApis.Models;
using Microsoft.Azure.Cosmos;
using StreamApis.Cosmos;

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
            var resultIterator = _container.GetItemQueryIterator<QuotesDocument>(query);
            var results = new List<QuotesDocument>();

            while (resultIterator.HasMoreResults)
            {
                var response = await resultIterator.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results.Select(qc => qc.Category).ToList();
        }

        public async Task AddQuote(Quote quote)
        {
            // Get the document
            var query = new QueryDefinition("select * from Quotes q where q.tenant = @tenant and q.category = @category")
                .WithParameter("@tenant", quote.Tenant)
                .WithParameter("@category", quote.Category);
            var resultIterator = _container.GetItemQueryIterator<QuotesDocument>(query);

            while (resultIterator.HasMoreResults)
            {
                var response = await resultIterator.ReadNextAsync();
                var etag = response.ETag;
                var resource = response.Resource.First();

                // Add the quote
                var id = resource.Quotes.Max(q => q.Id) + 1;

                resource.Quotes.Add(new QuotesDocument.Quote
                {
                    Id = id,
                    Tenant = quote.Tenant,
                    Category = quote.Category,
                    Who = quote.Who,
                    When = quote.When,
                    QuoteString = quote.QuoteString,
                });

                // Replace the document
                await _container.UpsertItemAsync(resource, requestOptions: new ItemRequestOptions { IfMatchEtag = resource.ETag });
            }
        }

        public async Task<List<Quote>> GetQuotes(string tenant)
        {
            var query = new QueryDefinition("select * from Quotes q where q.tenant = @tenant")
                .WithParameter("@tenant", tenant);
            var resultIterator = _container.GetItemQueryIterator<QuotesDocument>(query);
            var results = new List<QuotesDocument>();

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
            var resultIterator = _container.GetItemQueryIterator<QuotesDocument>(query);
            var results = new List<QuotesDocument>();

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
