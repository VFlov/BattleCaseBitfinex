using BattleCaseBitfinex.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BattleCaseBitfinex.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<FuturesPrice> PriceDifferences { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FuturesPrice>()
                .HasKey(p => new { p.Timestamp, p.Symbol });
        }
    }
}
