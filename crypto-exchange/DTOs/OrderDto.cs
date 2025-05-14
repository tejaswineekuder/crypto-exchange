using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_exchange.DTOs
{
    public class OrderDto
    {
        public string? Id { get; set; }
        public DateTime? Time { get; set; }
        public string? Type { get; set; }
        public string? Kind { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Price { get; set; }
    }
}
