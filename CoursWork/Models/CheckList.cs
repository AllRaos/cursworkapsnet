using System.ComponentModel.DataAnnotations;

namespace CoursWork.Models
{
    public class CheckList
    {
        [Key] 
        public int CheckId { get; set; }
        public Customers? CustomerId { get; set; }
        public Customers? Customers { get; set; }
        public DeliveryList? DeliveryList {  get; set; }
        public Orders? Orders { get; set; }
        public DeliveryList? ReceivingDate { get; set; }
    }
}
