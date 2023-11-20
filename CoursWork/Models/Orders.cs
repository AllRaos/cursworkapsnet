using System.ComponentModel.DataAnnotations;

namespace CoursWork.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public Customers? CustomerId { get; set; }
        public DateTime AcceptanceDate { get; set; } = DateTime.Now; 
    }
}
