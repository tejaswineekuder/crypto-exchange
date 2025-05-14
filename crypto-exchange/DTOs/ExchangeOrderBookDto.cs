using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_exchange.DTOs
{
    public class ExchangeOrderBookDto
    {
        [JsonProperty("Id")]
        public string? Id { get; set; }

        [JsonProperty("AvailableFunds")]
        public AvailableFunds? AvailableFunds { get; set; }

        [JsonProperty("OrderBook")]
        public OrderBook? OrderBook { get; set; }
    }

    public class AvailableFunds
    {
        [JsonProperty("Crypto")]
        public decimal? Crypto { get; set; }

        [JsonProperty("Euro")]
        public decimal? Euro { get; set; }
    }

    public class Ask
    {
        [JsonProperty("Order")]
        public OrderDto? Order { get; set; }
    }

    public class Bid
    {
        [JsonProperty("Order")]
        public OrderDto? Order { get; set; }
    }

    public class OrderBook
    {
        [JsonProperty("Bids")]
        public List<Bid>? Bids { get; set; }

        [JsonProperty("Asks")]
        public List<Ask>? Asks { get; set; }
    }

}
