using System.Threading;
using System.Threading.Tasks;
using Marketplace.Application.Events;
using Marketplace.Baskets.Events;
using Marketplace.Domain.Events;
using Marketplace.Logging;

namespace Marketplace.Baskets.DomainEvents
{
    public class BasketItemQuantityChangedEventHandler : IEventHandler<BasketItemQuantityChangedEvent>
    {
        public Task Handle(EventNotification<BasketItemQuantityChangedEvent> notification, CancellationToken cancellationToken)
        {
            LogHelper.AddInfo("BasketItemQuantityChangedEvent calisti.");
            
            return Task.FromResult(0);
        }
    }
}