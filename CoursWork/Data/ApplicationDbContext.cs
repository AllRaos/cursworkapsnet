using CoursWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursWork.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
                
        }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<CourierInfo> CourierInfo { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<DeliveryList> DeliveryList { get; set; }
        public DbSet<OrdersProducts> OrdersProducts { get; set; }
        public DbSet<CheckList> CheckList { get; set; }
    }
}
