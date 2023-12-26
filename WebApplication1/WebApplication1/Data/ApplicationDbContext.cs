using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CourierInfo> CourierInfo { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DeliveryList> DeliveryList { get; set; }
        public DbSet<OrdersProducts> OrdersProducts { get; set; }
        public DbSet<CheckList> CheckList { get; set; }
    }
}