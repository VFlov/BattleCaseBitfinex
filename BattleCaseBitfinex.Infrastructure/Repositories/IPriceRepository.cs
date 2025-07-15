using BattleCaseBitfinex.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCaseBitfinex.Infrastructure.Repositories
{
    public interface IPriceRepository
    {
        Task SavePriceDifferenceAsync(FuturesPrice price);
        Task<decimal?> GetLastAvailablePriceAsync(string symbol, DateTime before);
        Task<IEnumerable<FuturesPrice>> GetAllPriceDifferencesAsync();
    }

}
