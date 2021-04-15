using System;
using Marketplace.Domain.Events;

namespace Marketplace.Baskets.Events
{
    public class BasketItemAddedEvent : DomainEventBase
    {
        public int CustomerId { get; private set; }
        public int ItemId { get; private set; }
        public int Quantity { get; private set; }

        public BasketItemAddedEvent(int customerId, int itemId, int quantity)
        {
            CustomerId = customerId;
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}