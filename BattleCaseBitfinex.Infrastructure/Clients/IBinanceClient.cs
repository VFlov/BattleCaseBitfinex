using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCaseBitfinex.Infrastructure.Clients
{
    public interface IBinanceClient
    {
        Task<decimal?> GetPriceAsync(string symbol);
    }
}
