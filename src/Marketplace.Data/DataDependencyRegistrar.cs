using Autofac;
using Marketplace.Configuration;
using Marketplace.Data.Repositories;
using Marketplace.Dependency;
using Marketplace.Domain.Repositories;
using Marketplace.Reflection;

namespace Marketplace.Data
{
    public class DataDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, AppConfig appConfig)
        {
            builder.RegisterType<MongoDbContext>()
                .As<IMongoDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(MongoDbRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
        }

        public int Order => 4;
    }
}