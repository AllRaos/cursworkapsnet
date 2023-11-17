using System.ComponentModel.DataAnnotations;

namespace CoursWork.Models
{
    public class DeliveryList
    {
        [Key]
        public int DeliveryId { get; set; }
        public Orders Order_Id { get; set; }
        public CourierInfo CourierId { get; set; }
        public DateTime DateArrived { get; set; } = DateTime.Now;
        public string? Taken { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
