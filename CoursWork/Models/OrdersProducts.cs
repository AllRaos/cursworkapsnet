namespace CoursWork.Models
{
    public class OrdersProducts
    {
        public Orders? OrderId { get; set; }
        public Products? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
