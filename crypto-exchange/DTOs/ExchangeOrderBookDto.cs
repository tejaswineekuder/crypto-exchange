using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_exchange.DTOs
{
    public class ExchangeOrderBookDto
    {
        public string? Id { get; set; }
        public AvailableFunds? AvailableFunds { get; set; }
        public OrderBook? OrderBook { get; set; }
    }

    public class AvailableFunds
    {
        public decimal? Crypto { get; set; }
        public decimal? Euro { get; set; }
    }

    public class Ask
    {
        public OrderDto? Order { get; set; }
    }

    public class Bid
    {
        public OrderDto? Order { get; set; }
    }

    public class OrderBook
    {
        public List<Bid>? Bids { get; set; }
        public List<Ask>? Asks { get; set; }
    }

}
