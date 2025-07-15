using BattleCaseBitfinex.Services;
using Quartz;

namespace BattleCaseBitfinex.Quartz
{
    public class PriceDifferenceJob : IJob
    {
        private readonly IFuturesPriceService _futuresPriceService;

        public PriceDifferenceJob(IFuturesPriceService futuresPriceService)
        {
            _futuresPriceService = futuresPriceService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _futuresPriceService.CalculateAndStorePriceDifferenceAsync(
                "BTCUSDT",
                "ETHBTC",
                TimeSpan.FromHours(1));
        }
    }
}
