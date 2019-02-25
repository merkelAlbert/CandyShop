using Microsoft.EntityFrameworkCore;

namespace ConsoleCandyShop.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Pastry> Pastries { get; set; }
    }
}