using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models
{
    public class DeliveryList
    {
        [Key]
        public int DeliveryId { get; set; }
        public int? CourierId { get; set; }
        public CourierInfo? CourierInfo { get; set; }
        public DateTime ReceivingDate { get; set; } = DateTime.Now;
        public string? Status { get; set; }
        public float? Total { get; set; }
        public Order? Order { get; set; }
    }
}
