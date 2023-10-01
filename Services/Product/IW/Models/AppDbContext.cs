using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
           .HasOne<Category>(s => s.Category)
           .WithMany(g => g.Products)
           .HasForeignKey(s => s.CategoryId);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories{ get; set; }
    }
}
