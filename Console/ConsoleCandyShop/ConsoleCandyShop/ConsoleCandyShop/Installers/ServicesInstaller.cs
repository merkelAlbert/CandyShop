using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ConsoleCandyShop.Interceptors;
using ConsoleCandyShop.Interfaces;
using ConsoleCandyShop.Services;

namespace ConsoleCandyShop.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUsersService>().ImplementedBy<UsersService>().LifestyleTransient(),
                Component.For<IPastriesService>().ImplementedBy<PastriesService>().LifestyleTransient(),
                Component.For<IOrdersService>().ImplementedBy<OrdersService>().LifestyleTransient());
        }
    }
}