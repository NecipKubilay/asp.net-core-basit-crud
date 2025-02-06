using BasitAlisveris.Models;
using Microsoft.EntityFrameworkCore;

namespace BasitAlisveris.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            /*Database.Migrate(); */// Veritabanını otomatik olarak günceller
        }

        


        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Admin> Admins { get; set; }
    }
}
