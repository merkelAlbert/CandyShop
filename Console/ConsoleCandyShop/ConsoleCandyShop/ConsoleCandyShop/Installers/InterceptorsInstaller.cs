using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ConsoleCandyShop.Interceptors;

namespace ConsoleCandyShop.Installers
{
    public class InterceptorsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<BenchmarkInterceptor>().LifestyleTransient()
                    .Named("Benchmark"));
        }
    }
}