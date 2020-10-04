using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Nullforce.StreamApis.Currencies
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : Controller
    {
        private readonly ILogger<CurrenciesController> _logger;

        public CurrenciesController()
        {
        }

        [HttpGet]
        public async Task<ActionResult> GetCurrencies()
        {
            throw new NotImplementedException();
        }
    }
}