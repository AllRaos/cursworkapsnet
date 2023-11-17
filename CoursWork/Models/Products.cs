using System.ComponentModel.DataAnnotations;

namespace CoursWork.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string? MenuName { get; set; }
        public decimal Price { get; set; }
    }
}
