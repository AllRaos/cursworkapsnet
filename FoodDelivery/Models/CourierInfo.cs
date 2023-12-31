using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Models
{
    public class CourierInfo
    {
        [Key]
        public int CourierId { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public string? PhoneNumber { get; set; }
        public float? DeliveryPay { get; set; }
        public ICollection<DeliveryList>? DeliveryLists { get; set;}
    }

}
