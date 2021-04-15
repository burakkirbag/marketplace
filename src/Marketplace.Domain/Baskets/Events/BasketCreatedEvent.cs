using System;
using Marketplace.Domain.Events;

namespace Marketplace.Baskets.Events
{
    public class BasketCreatedEvent : DomainEventBase
    {
        public int CustomerId { get; private set; }

        public BasketCreatedEvent(int customerId)
        {
            CustomerId = customerId;
        }
    }
}