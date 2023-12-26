using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class DeliveryList
    {
        [Key]
        public int DeliveryId { get; set; }
        public Order? OrderId { get; set; }
        public Order? Orders { get; set; }
        public CourierInfo? CourierId { get; set; }
        public CourierInfo? CourierInfo { get; set; }
        public DateTime ReceivingDate { get; set; } = DateTime.Now;
        public string? Status { get; set; }
        public decimal? Total { get; set; }
    }
}
