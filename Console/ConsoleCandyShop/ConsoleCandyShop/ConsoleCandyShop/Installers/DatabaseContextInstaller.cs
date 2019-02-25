using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ConsoleCandyShop.DAL;
using Microsoft.EntityFrameworkCore;

namespace ConsoleCandyShop.Installers
{
    public class DatabaseContextInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=Technic;";
            container.Register(Component.For<DbContext>().DependsOn(new
            {
                connectionString = connectionString
            }).ImplementedBy<DatabaseContext>().LifestylePerThread());
        }
    }
}