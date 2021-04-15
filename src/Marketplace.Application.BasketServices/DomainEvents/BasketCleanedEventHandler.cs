using System.Threading;
using System.Threading.Tasks;
using Marketplace.Application.Events;
using Marketplace.Baskets.Events;
using Marketplace.Logging;

namespace Marketplace.Baskets.DomainEvents
{
    public class BasketCleanedEventHandler : IEventHandler<BasketCleanedEvent>
    {
        public Task Handle(EventNotification<BasketCleanedEvent> notification, CancellationToken cancellationToken)
        {
            LogHelper.AddInfo("BasketCleanedEventHandler calisti.");

            
            return Task.FromResult(0);
        }
    }
}