using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Street { get; set; }
        public int? House { get; set; }    
        public ICollection<OrderProduct>? OrderProducts { get; set;}
    }
}
