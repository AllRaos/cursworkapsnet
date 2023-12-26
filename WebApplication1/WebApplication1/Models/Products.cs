using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public int? Quantity { get; set; }
        public string? Status { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
