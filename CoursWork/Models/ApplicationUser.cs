using Microsoft.AspNetCore.Identity;

namespace CoursWork.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
        public Customers? Customer { get; set; }
    }
}
