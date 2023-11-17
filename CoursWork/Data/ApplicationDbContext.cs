using CoursWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursWork.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
                
        }
        public DbSet<Customers>Customers    
    }
}
