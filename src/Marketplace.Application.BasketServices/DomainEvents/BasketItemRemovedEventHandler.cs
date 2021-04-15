using System.Threading;
using System.Threading.Tasks;
using Marketplace.Application.Events;
using Marketplace.Baskets.Events;
using Marketplace.Logging;

namespace Marketplace.Baskets.DomainEvents
{
    public class BasketItemRemovedEventHandler : IEventHandler<BasketItemRemovedEvent>
    {
        public Task Handle(EventNotification<BasketItemRemovedEvent> notification, CancellationToken cancellationToken)
        {
            LogHelper.AddInfo("BasketItemRemovedEventHandler calisti.");

            return Task.FromResult(0);
        }
    }
}