using System.ComponentModel.DataAnnotations;

namespace crypto_exchange_web.DTOs
{
    public class RequestDto
    {
        [Required]
        public decimal? OrderAmount { get; set; }

        [Required]
        public string? OrderType { get; set; }
    }
}
