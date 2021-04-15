using Autofac;
using Marketplace.Configuration;
using Marketplace.Reflection;

namespace Marketplace.Dependency
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, AppConfig appConfig);

        int Order { get; }
    }
}