using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CourierInfo
    {
        [Key]
        public int CourierId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public float? DeliveryPay { get; set; }
        public ICollection<DeliveryList>? DeliveryLists { get; set;}
    }
}
