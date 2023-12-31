using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime AcceptanceDate { get; set; } = DateTime.Now;
        public Customer? Customers { get; set; }
        public int? DeliveryListId { get; set; }
        public DeliveryList? DeliveryList { get; set; }
        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }

}
