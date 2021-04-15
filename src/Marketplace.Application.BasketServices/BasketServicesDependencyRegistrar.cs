using Autofac;
using Marketplace.Baskets.Rules;
using Marketplace.Configuration;
using Marketplace.Dependency;
using Marketplace.Baskets.DomainServices;
using Marketplace.Reflection;

namespace Marketplace.Baskets
{
    public class BasketServicesDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, AppConfig appConfig)
        {
            builder.RegisterType<ItemStockChecker>()
                .As<IItemStockChecker>()
                .InstancePerLifetimeScope();
        }

        public int Order => 5;
    }
}