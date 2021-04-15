using System;
using Marketplace.Domain.Events;

namespace Marketplace.Baskets.Events
{
    public class BasketItemQuantityChangedEvent : DomainEventBase
    {
        public int CustomerId { get; private set; }
        public int ItemId { get; private set; }
        public int OldQuantity { get; private set; }
        public int NewQuantity { get; private set; }

        public BasketItemQuantityChangedEvent(int customerId, int itemId, int oldQuantity, int newQuantity)
        {
            CustomerId = customerId;
            ItemId = itemId;
            OldQuantity = oldQuantity;
            NewQuantity = newQuantity;
        }
    }
}