using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StreamApis.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nullforce.StreamApis.Quotes
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        private readonly ILogger<QuotesController> _logger;
        private readonly IQuotesRepository _quotesRepository;

        public QuotesController(
            ILogger<QuotesController> logger,
            IQuotesRepository quotesRepository)
        {
            _logger = logger;
            _quotesRepository = quotesRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<QuoteViewModel>> GetQuotes()
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "-1";
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "Unknown";

            var quotes = await _quotesRepository.GetQuotes(userid);

            return quotes.Select(q => new QuoteViewModel
            {
                Category = q.Category,
                Who = q.Who,
                When = q.When,
                Quote = q.QuoteString,
            });
        }

        [Authorize]
        [HttpGet("categories/{categoryId}")]
        public async Task<IEnumerable<QuoteViewModel>> GetQuotesByCategory(string categoryId)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "-1";
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "Unknown";

            var quotes = await _quotesRepository.GetQuotes(userid, categoryId);

            return quotes.Select(q => new QuoteViewModel
            {
                Category = q.Category,
                Who = q.Who,
                When = q.When,
                Quote = q.QuoteString,
            });
        }

        [Authorize]
        [HttpGet("categories")]
        public async Task<QuoteCategoriesViewModel> GetQuoteCategories()
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "-1";
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "Unknown";

            var categories = await _quotesRepository.GetCategories(userid);

            return new QuoteCategoriesViewModel
            {
                Categories = categories.ToArray(),
            };
        }
    }
}
