using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_exchange.DTOs
{
    public class ExecutionPlanDto
    {
        [JsonProperty("Id")]
        public string? Id { get; set; }

        [JsonProperty("Type")]
        public string? Type { get; set; }

        [JsonProperty("Amount")]
        public decimal? Amount { get; set; }

        [JsonProperty("Price")]
        public decimal? Price { get; set; }

        [JsonProperty("Plan")]
        public string? Plan
        {
            get
            {
                return $"Execution Plan :  {Type} {Amount} BTC {Id} at {Price} EUR each.";
            }
        }
    }
}
