using System.ComponentModel.DataAnnotations;

namespace StockMarket.Dtos
{
    public class CreateStockDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 char")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "Company Name cannot be over 10 char")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1,1000000000)]
        public decimal Purchanse { get; set; }
        [Required]
        [Range(0.001, 1000)]
        public decimal LastDiv { get; set; }
        [Required]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }
    }
}
