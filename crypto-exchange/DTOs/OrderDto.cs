using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_exchange.DTOs
{
    public class OrderDto
    {
        [JsonProperty("Id")]
        public string? Id { get; set; }

        [JsonProperty("Time")]
        public DateTime? Time { get; set; }

        [JsonProperty("Type")]
        public string? Type { get; set; }

        [JsonProperty("Kind")]
        public string? Kind { get; set; }

        [JsonProperty("Amount")]
        public decimal? Amount { get; set; }

        [JsonProperty("Price")]
        public decimal? Price { get; set; }
    }
}
