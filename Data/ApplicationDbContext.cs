using Microsoft.EntityFrameworkCore;
using BaiTap_23WebC_Nhom10.Models;
namespace BaiTap_23WebC_Nhom10.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.slug)
                .IsUnique();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        
        public DbSet<Category> Categories { get; set; }
        
    }
}