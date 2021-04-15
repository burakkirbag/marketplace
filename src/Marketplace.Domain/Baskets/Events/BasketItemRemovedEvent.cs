using System;
using Marketplace.Domain.Events;

namespace Marketplace.Baskets.Events
{
    public class BasketItemRemovedEvent : DomainEventBase
    {
        public int CustomerId { get; private set; }

        public int ItemId { get; private set; }

        public BasketItemRemovedEvent(int customerId, int itemId)
        {
            CustomerId = customerId;
            ItemId = itemId;
        }
    }
}