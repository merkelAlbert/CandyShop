using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.Interceptors;
using ConsoleCandyShop.Interfaces;
using ConsoleCandyShop.Services;
using Microsoft.EntityFrameworkCore;

namespace ConsoleCandyShop.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<DatabaseContext>().UsingFactoryMethod(c=>new  DatabaseContextFactory().CreateDbContext(null)),
                Component.For<IUsersService>().ImplementedBy<UsersService>().LifestyleTransient(),
                Component.For<IPastriesService>().ImplementedBy<PastriesService>().LifestyleTransient(),
                Component.For<IOrdersService>().ImplementedBy<OrdersService>().LifestyleTransient());
        }
    }
}