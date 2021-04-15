using System.Threading;
using System.Threading.Tasks;
using Marketplace.Application.Events;
using Marketplace.Baskets.Events;
using Marketplace.Logging;

namespace Marketplace.Baskets.DomainEvents
{
    public class BasketItemAddedEventHandler : IEventHandler<BasketItemAddedEvent>
    {
        public Task Handle(EventNotification<BasketItemAddedEvent> notification, CancellationToken cancellationToken)
        {
            LogHelper.AddInfo("BasketItemAddedEventHandler calisti.");
            
            return Task.FromResult(0);
        }
    }
}