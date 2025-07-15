using BattleCaseBitfinex.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCaseBitfinex.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=qwe123"; //Да, дублирование :(
            optionsBuilder.UseNpgsql(connectionString);
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
