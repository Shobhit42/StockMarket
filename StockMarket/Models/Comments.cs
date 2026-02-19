using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket.Models
{
    [Table("Comment")]
    public class Comments
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockID { get; set; }
        public Stock? Stock { get; set; }
        public string AppUerId { get; set; }
        public AppUser AppUser { get; set; }
    }
}