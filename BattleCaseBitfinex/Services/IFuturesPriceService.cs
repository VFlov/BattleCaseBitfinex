namespace BattleCaseBitfinex.Services
{
    public interface IFuturesPriceService
    {
        Task CalculateAndStorePriceDifferenceAsync(string symbol1, string symbol2, TimeSpan interval);
    }
}
