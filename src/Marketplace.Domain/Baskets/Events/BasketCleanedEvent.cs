using System;
using Marketplace.Domain.Events;

namespace Marketplace.Baskets.Events
{
    public class BasketCleanedEvent : DomainEventBase
    {
        public int CustomerId { get; private set; }

        public BasketCleanedEvent(int customerId)
        {
            CustomerId = customerId;
        }
    }
}