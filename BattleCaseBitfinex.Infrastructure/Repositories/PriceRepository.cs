using BattleCaseBitfinex.Data;
using BattleCaseBitfinex.Domain;
using BattleCaseBitfinex.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCaseBitfinex.Infrastructure.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        private readonly AppDbContext _context;

        public PriceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SavePriceDifferenceAsync(FuturesPrice price)
        {
            _context.PriceDifferences.Add(price);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<FuturesPrice>> GetAllPriceDifferencesAsync()
        {
            var prices = await _context.PriceDifferences.Select(o => o).ToListAsync() ;

            return prices;
        }

        public async Task<decimal?> GetLastAvailablePriceAsync(string symbol, DateTime before)
        {
            var price = await _context.PriceDifferences
                .Where(p => p.Symbol == symbol && p.Timestamp <= before)
                .OrderByDescending(p => p.Timestamp)
                .Select(p => p.PriceDifference)
                .FirstOrDefaultAsync();

            return price;
        }
    }
}
