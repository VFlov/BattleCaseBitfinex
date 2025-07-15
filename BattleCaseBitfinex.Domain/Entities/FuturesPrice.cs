using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCaseBitfinex.Domain.Entities
{
    public class FuturesPrice
    {
        public DateTime Timestamp { get; set; }
        public string Symbol { get; set; }
        /// <summary>
        /// Цена первого фьючерса, для которого рассчитывается разница
        /// </summary>

        public decimal Price { get; set; }
        /// <summary>
        /// Разница
        /// </summary>
        public decimal PriceDifference { get; set; }
    }
}
