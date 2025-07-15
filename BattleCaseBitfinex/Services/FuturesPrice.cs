using BattleCaseBitfinex.Domain.Entities;
using BattleCaseBitfinex.Infrastructure.Clients;
using BattleCaseBitfinex.Infrastructure.Repositories;

namespace BattleCaseBitfinex.Services
{
    public class FuturesPriceService : IFuturesPriceService
    {
        private readonly IPriceRepository _priceRepository;
        private readonly IBinanceClient _binanceClient;
        private readonly ILogger<FuturesPriceService> _logger;

        public FuturesPriceService(IPriceRepository priceRepository, IBinanceClient binanceClient, ILogger<FuturesPriceService> logger)
        {
            _priceRepository = priceRepository;
            _binanceClient = binanceClient;
            _logger = logger;
        }

        public async Task CalculateAndStorePriceDifferenceAsync(string symbol1, string symbol2, TimeSpan interval)
        {
            try
            {
                var price1 = await GetPriceWithFallbackAsync(symbol1, interval);
                var price2 = await GetPriceWithFallbackAsync(symbol2, interval);

                var priceDifference = new FuturesPrice
                {
                    Timestamp = DateTime.UtcNow,
                    Symbol = $"{symbol1}-{symbol2}",
                    Price = price1, // Сохраняем цену первого фьючерса
                    PriceDifference = Math.Abs(price1 - price2)
                };

                await _priceRepository.SavePriceDifferenceAsync(priceDifference);
                _logger.LogInformation("Price difference calculated and stored: {PriceDifference}, Price: {Price}", priceDifference.PriceDifference, priceDifference.Price);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating price difference for {Symbol1} and {Symbol2}", symbol1, symbol2);
                throw;
            }
        }

        private async Task<decimal> GetPriceWithFallbackAsync(string symbol, TimeSpan interval)
        {
            var price = await _binanceClient.GetPriceAsync(symbol);
            if (price != null)
                return price.Value;

            var previousInterval = DateTime.UtcNow - interval;
            price = await _priceRepository.GetLastAvailablePriceAsync(symbol, previousInterval);
            return price ?? 0;
        }
    }
}
