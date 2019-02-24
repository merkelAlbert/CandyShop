using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Diagnostics.Extensions;
using Castle.Windsor.Installer;

namespace ConsoleCandyShop.Installers
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyNamed("ConsoleCandyShop")
                    .InNamespace("ConsoleCandyShop.Controllers")
                    .If(c => c.Name.EndsWith("Controller")).LifestyleTransient());
        }
    }
}