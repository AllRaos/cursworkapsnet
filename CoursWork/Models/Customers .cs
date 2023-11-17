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
        public string? District { get; set; }
        public string? Street { get; set; }
        public int House { get; set; }
        public int Apartment { get; set; }
    }
}
