using CandyShop.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CandyShop.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}