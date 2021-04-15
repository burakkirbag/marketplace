using Autofac;
using Marketplace.Configuration;
using Marketplace.Dependency;
using Marketplace.Domain.Events;
using Marketplace.Net.Mail;
using Marketplace.Reflection;

namespace Marketplace
{
    public class CoreDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, AppConfig appConfig)
        {
            builder.RegisterType<EmailSender>()
                .As<IEmailSender>()
                .InstancePerLifetimeScope();
        }

        public int Order => 1;
    }
}