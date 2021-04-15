using Autofac;
using Marketplace.Configuration;
using Marketplace.Dependency;
using Marketplace.Reflection;

namespace Marketplace
{
    public class DomainDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, AppConfig appConfig)
        {
        }

        public int Order => 3;
    }
}