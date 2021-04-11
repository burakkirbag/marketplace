using Marketplace.Application.Events;
using Marketplace.Products.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Application.ProductServices.DomainEventHandlers
{
    class ProductCreatedDomainEventHandler : IDomainEventHandler<ProductCreatedEvent>
    {
        public Task Handle(DomainEventNotification<ProductCreatedEvent> notification, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
