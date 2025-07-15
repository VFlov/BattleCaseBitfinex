using BattleCaseBitfinex.Domain.Entities;
using BattleCaseBitfinex.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BattleCaseBitfinex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceDifferencesController : ControllerBase
    {
        private readonly IPriceRepository _priceRepository;

        public PriceDifferencesController(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuturesPrice>>> GetAllPriceDifferences()
        {
            var prices = await _priceRepository.GetAllPriceDifferencesAsync();
            return Ok(prices);
        }
    }

}
