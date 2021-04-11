using Marketplace.Domain.Events;

namespace Marketplace.Products.Events
{
    public class ProductCreatedEvent : DomainEventBase
    {
        public Product Product { get; }

        public ProductCreatedEvent(Product product)
        {
            Product = product;
        }
    }
}
