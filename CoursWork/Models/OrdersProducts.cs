using System.ComponentModel.DataAnnotations;


namespace CoursWork.Models
{
    public class OrdersProducts
    {
        [Key]
        public int OrdersProductsId { get; set; }
        public int OrderId { get; set; }
        public Orders? Orders { get; set; }
        public int ProductId { get; set; }
        public Products? Products { get; set; }
        public int Quantity { get; set; }
    }
}
