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
            modelBuilder.Entity<User>()
           .HasOne<Role>(s => s.Role)
           .WithMany(g => g.Users)
           .HasForeignKey(s => s.RoleId);

            modelBuilder.Entity<Address>()
                .HasOne(s => s.User)
                .WithMany(a => a.Addresses)
                .HasForeignKey(s=>s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
