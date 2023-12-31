using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models
{
    public class OrderProduct
    {
        [Key]
        public int? OrderProductId { get; set; }
        public int? Quantity { get; set; }
        public float? TotalPrice { get; set; }
        public int? TotalWeight { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
