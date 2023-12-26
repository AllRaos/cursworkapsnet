using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public Customer? CustomerId { get; set; }
        public Customer? Customers { get; set; }
        public DateTime AcceptanceDate { get; set; } = DateTime.Now;
    }
}
