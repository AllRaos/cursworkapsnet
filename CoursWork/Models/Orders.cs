using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations;

namespace CoursWork.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateGet { get; set; } = DateTime.Now; 
    }
}
