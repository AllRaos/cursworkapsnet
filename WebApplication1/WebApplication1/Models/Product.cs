using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }
        public string? Category { get; set; }
        public int? Netto { get; set; }
        public string? Status { get; set; }
        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
