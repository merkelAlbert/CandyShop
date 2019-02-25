using Microsoft.EntityFrameworkCore;

namespace ConsoleCandyShop.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        DbSet<User> Users { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Pastry> Pastries { get; set; }
    }
}