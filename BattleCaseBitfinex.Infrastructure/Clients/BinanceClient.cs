using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCaseBitfinex.Infrastructure.Clients
{
    public class BinanceClient : IBinanceClient
    {
        private readonly RestClient _client;

        public BinanceClient()
        {
            _client = new RestClient("https://api.binance.com");
        }

        public async Task<decimal?> GetPriceAsync(string symbol)
        {
            // Настоящая цена
            var realTimeRequest = new RestRequest($"/api/v3/ticker?symbol={symbol}", Method.Get);
            var realTimeResponse = await _client.ExecuteAsync(realTimeRequest);
            if (realTimeResponse.IsSuccessful)
            {
                // Поле "цена"
                var json = System.Text.Json.JsonDocument.Parse(realTimeResponse.Content);
                if (json.RootElement.TryGetProperty("price", out var priceElement))
                    return decimal.Parse(priceElement.GetString());
            }

            // Цена за 24ч
            var dailyRequest = new RestRequest($"/api/v3/ticker/24hr?symbol={symbol}", Method.Get);
            var dailyResponse = await _client.ExecuteAsync(dailyRequest);
            if (dailyResponse.IsSuccessful)
            {
                // Поле "последняя цена"
                var json = System.Text.Json.JsonDocument.Parse(dailyResponse.Content);
                if (json.RootElement.TryGetProperty("lastPrice", out var priceElement))
                    return decimal.Parse(priceElement.GetString().Replace(".",",")); //В идеале нужно использовать CultureInfo.InvariantCulture, но для простоты заменим точку на запятую
            }

            return null;
        }
    }
}
