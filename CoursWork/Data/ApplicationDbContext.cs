using CoursWork.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoursWork.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<CourierInfo> CourierInfo { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<DeliveryList> DeliveryList { get; set; }
        public DbSet<OrdersProducts> OrdersProducts { get; set; }
        public DbSet<CheckList> CheckList { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


            //add AspNetUsers connection with my own database

            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

        }
        public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
        {
            public void Configure(EntityTypeBuilder<ApplicationUser> builder)
            {
                builder.Property(u => u.LastName);
                builder.Property(u => u.FirstName);
            }

        }
    }
}
