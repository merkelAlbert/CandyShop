using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.Interfaces;
using ConsoleCandyShop.Services;

namespace ConsoleCandyShop.Installers
{
    public class DatabaseContextInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<DatabaseContext>()
                    .UsingFactoryMethod(c => new DatabaseContextFactory().CreateDbContext(null)));
        }
    }
}