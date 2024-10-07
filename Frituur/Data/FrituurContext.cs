using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Frituur.Models;

namespace Frituur.Data
{
    public class FrituurContext : DbContext
    {
        public FrituurContext(DbContextOptions<FrituurContext> options)
            : base(options)
        {
        }
        public DbSet<Frituur.Models.Product> Product { get; set; } = default!;
        public DbSet<Frituur.Models.User> User { get; set; } = default!;
        public DbSet<Frituur.Models.Order> Order { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId);
        }
    }
}

