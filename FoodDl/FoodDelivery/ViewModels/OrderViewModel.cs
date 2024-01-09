using System.Collections.Generic;

namespace FoodDelivery.ViewModels
{
    public class OrderViewModel
    {
        public List<OrderProductViewModel>? OrderProducts { get; set; }
        public float? TotalPrice { get; set; }
    }

    public class OrderProductViewModel
    {
        public int? ProductId { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }
        public string? ImageUrl { get; set; }
        public int? Quantity { get; set; }
        public float? TotalPrice { get; set; }
    }
}
