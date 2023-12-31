using FoodDelivery.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    public string? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Street { get; set; }
    public int? House { get; set; }
    public ICollection<OrderProduct>? OrderProducts { get; set; }
}

