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
            modelBuilder.Entity<Transaction>()
            .HasOne<Inventory>(s => s.Inventory)
            .WithMany(g => g.Transactions)
            .HasForeignKey(s => s.InventoryId);
        }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
