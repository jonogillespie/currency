using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UseCases.Currencies;
using Application.UseCases.Currencies.Dto;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;
// ReSharper disable RouteTemplates.RouteParameterConstraintNotResolved

namespace Presentation.Controllers
{
    [Route("v{version:apiVersion}/currencies")]
    [ApiController]
    public class CurrenciesController : AppController
    {
        [HttpGet]
        public Task<ActionResult<CurrencyVm>> Get() 
            => Query(new GetAllCurrenciesQuery());

        [HttpGet("abbreviations")]
        public Task<ActionResult<List<string>>> GetAllCurrencies()
            => Query(new GetAllCurrencyAbbreviationsQuery());

        [HttpGet("{symbol}/conversions/{amount:double}")]
        public Task<ActionResult<double>> GetConversionAmount(string symbol, double amount) 
            => Query(new GetBitcoinConversionQuery(symbol, amount));
    }
}