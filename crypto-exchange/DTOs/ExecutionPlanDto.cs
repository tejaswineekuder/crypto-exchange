using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_exchange.DTOs
{
    public class ExecutionPlanDto
    {
        public string? Id { get; set; }
        public string? Type { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Price { get; set; }

        public string? Plan
        {
            get
            {
                return $"Execution Plan :  {Type} {Amount} BTC {Id} at {Price} EUR each.";
            }
        }
    }
}
