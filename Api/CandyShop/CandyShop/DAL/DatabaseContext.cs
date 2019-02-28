using CandyShop.DAL.Models;
using CandyShop.DAL.Models.IntermediateModels;
using Microsoft.EntityFrameworkCore;

namespace CandyShop.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Pastry> Pastries { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderPastry>()
                .HasOne(op => op.Order)
                .WithMany(o => o.Pastries)
                .HasForeignKey(op => op.OrderId);
            
            builder.Entity<OrderPastry>()
                .HasOne(op => op.Pastry)
                .WithMany(o => o.Orders)
                .HasForeignKey(op => op.PastryId);
        }
    }
}