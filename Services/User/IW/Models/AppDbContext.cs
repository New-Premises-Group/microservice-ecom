﻿using IW.Models.Entities;
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
           .WithMany(g => g.User)
           .HasForeignKey(s => s.RoleId);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
