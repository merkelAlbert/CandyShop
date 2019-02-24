using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ConsoleCandyShop.DAL;
using ConsoleCandyShop.Interfaces;
using ConsoleCandyShop.Services;

namespace ConsoleCandyShop.Installers
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<Repository>().LifestyleSingleton());
        }
    }
}