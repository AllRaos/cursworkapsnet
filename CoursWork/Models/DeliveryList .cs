using System.ComponentModel.DataAnnotations;

namespace CoursWork.Models
{
    public class DeliveryList
    {
        [Key]
        public int DeliveryId { get; set; }
        public Orders OrderId { get; set; }
        public CourierInfo CourierId { get; set; }
        public DateTime ReceivingDate { get; set; } = DateTime.Now;
        public string? Status { get; set; }
        public decimal? Total { get; set; }
    }
}
