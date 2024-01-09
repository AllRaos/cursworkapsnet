namespace FoodDelivery.ViewModels
{
    public class BasketViewModel
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public float? Price { get; set; }

        public string? ImageUrl { get; set; }

        public float? FinalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
