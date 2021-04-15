using System.Threading;
using System.Threading.Tasks;
using Marketplace.Application.Events;
using Marketplace.Baskets.Events;
using Marketplace.Logging;

namespace Marketplace.Baskets.DomainEvents
{
    public class BasketCreatedEventHandler : IEventHandler<BasketCreatedEvent>
    {
        public Task Handle(EventNotification<BasketCreatedEvent> notification, CancellationToken cancellationToken)
        {
            LogHelper.AddInfo("BasketCreatedEventHandler calisti.");

            return Task.FromResult(0);
        }
    }
}