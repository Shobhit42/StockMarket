using System.ComponentModel.DataAnnotations;

namespace StockMarket.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email {  get; set; }
        [Required]
        public string? Password { get; set; }

    }
}
