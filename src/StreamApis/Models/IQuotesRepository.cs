using System.Collections.Generic;
using System.Threading.Tasks;

namespace StreamApis.Models
{
    public interface IQuotesRepository
    {
        Task<List<string>> GetCategories(string tenant);

        Task<List<Quote>> GetQuotes(string tenant);
        Task<List<Quote>> GetQuotes(string tenant, string category);
    }
}
