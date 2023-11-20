using System.ComponentModel.DataAnnotations;

namespace CoursWork.Models
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Street { get; set; }
        public int House { get; set; }
    }
}
