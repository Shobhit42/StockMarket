using System.ComponentModel.DataAnnotations;

namespace StockMarket.Dtos
{
    public class UpdateCommentRequestDtos
    {
        [Required]
        [MinLength(280, ErrorMessage = "Title must be of 5 characters")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 200 charaters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(280, ErrorMessage = "Content must be of 5 characters")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 200 charaters")]
        public string Content { get; set; } = string.Empty;
    }
}
