using System.ComponentModel.DataAnnotations;

namespace CoursWork.Models
{
    public class OrdersProducts
    {
        [Key]
        public int OrdersProductsId { get; set; }
        public Orders? OrderId { get; set; }
        public Products? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
