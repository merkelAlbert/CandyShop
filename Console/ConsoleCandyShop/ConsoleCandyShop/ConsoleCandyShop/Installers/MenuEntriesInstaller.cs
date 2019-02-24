using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace ConsoleCandyShop.Installers
{
    public class MenuEntriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyNamed("ConsoleCandyShop")
                    .InNamespace("ConsoleCandyShop.MenuEntries")
                    .If(c => c.Name.EndsWith("MenuEntry")).LifestyleTransient());
        }
    }
}