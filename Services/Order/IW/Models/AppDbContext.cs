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
            modelBuilder.Entity<OrderItem>()
            .HasOne<Order>(s => s.Order)
            .WithMany(g => g.Items)
            .HasForeignKey(s => s.OrderId);
        }
        public DbSet<OrderItem> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Discount> Discounts { get; set; }
    }
}
