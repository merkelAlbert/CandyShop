using Microsoft.EntityFrameworkCore;

namespace ConsoleCandyShop.DAL
{
    public class DbContextFactoryBuilder
    {
        public static DbContext Create(string connectionString)
        {
            var result = new DatabaseContext(new DbContextOptionsBuilder().UseNpgsql(connectionString).Options);
            return result;
        }
    }
}