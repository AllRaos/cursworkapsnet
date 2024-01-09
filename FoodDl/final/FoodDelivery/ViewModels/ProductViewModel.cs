
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public string? Name { get; set; }

        public float? Price { get; set; }

        public string? Category { get; set; }

        public int? Netto { get; set; }

        public string? Status { get; set; }

        public string? ImageUrl { get; set; }
        
        public IFormFile? Image { get; set; }
    }
}
