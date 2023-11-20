using System.ComponentModel.DataAnnotations;

namespace CoursWork.Models
{
    public class CheckList
    {
        [Key] 
        public int CheckId { get; set; }
        public Customers? CustomerId { get; set; }
        public Customers? FirstName { get; set; }
        public Customers? LastName { get;set; }
        public DeliveryList? Total {  get; set; }
        public Orders? AcceptenceDate { get; set; }
        public DeliveryList? ReceivingDate { get; set; }
    }
}
