using System;
using System.Collections.Generic;
using System.Linq;
using Marketplace.Baskets.Events;
using Marketplace.Baskets.Exceptions;
using Marketplace.Baskets.Rules;
using Marketplace.Domain;
using Marketplace.Domain.Entities;

namespace Marketplace.Baskets
{
    public class Basket : EntityBase, IAggregateRoot
    {
        public virtual int CustomerId { get; protected set; }

        public virtual List<BasketItem> Items { get; protected set; }

        public Basket()
        {
            Items = new List<BasketItem>();
        }

        private Basket(int customerId)
        {
            CustomerId = customerId;
            Items = new List<BasketItem>();

            AddEvent(new BasketCreatedEvent(customerId));
        }

        public static Basket Create(int customerId)
        {
            var basket = new Basket(customerId);

            return basket;
        }

        public void AddItem(int itemId, int quantity, IItemStockChecker itemStockChecker)
        {
            if (quantity <= 0)
                throw new BasketInvalidItemQuantityException("Sepete eklemek istediğiniz ürün miktarını seçmelisiniz.");

            CheckRule(new ItemStockMustBeAvailableRule(itemStockChecker, itemId, quantity));

            if (AlreadyInBasketWithSameQuantity(itemId, quantity)) return;

            if (AlreadyInBasketButDifferentQuantity(itemId, quantity))
            {
                ChangeItemQuantity(itemId, quantity, itemStockChecker);
                return;
            }

            var item = BasketItem.Create(itemId, quantity);

            Items.Add(item);

            AddEvent(new BasketItemAddedEvent(CustomerId, itemId, quantity));
        }

        public void ChangeItemQuantity(int itemId, int quantity, IItemStockChecker itemStockChecker)
        {
            var item = Items.FirstOrDefault(x => x.ProductId == itemId);
            if (item == null)
                throw new BasketItemNotFoundException("Ürün sepetinizde mevcut değil.");

            if (quantity <= 0)
            {
                RemoveItem(itemId);
                return;
            }

            CheckRule(new ItemStockMustBeAvailableRule(itemStockChecker, itemId, quantity));

            var oldQuantity = item.Quantity;

            item.ChangeQuantity(quantity);

            AddEvent(new BasketItemQuantityChangedEvent(CustomerId, itemId, oldQuantity, quantity));
        }

        public void RemoveItem(int itemId)
        {
            var item = Items.FirstOrDefault(x => x.ProductId == itemId);
            if (item == null)
                throw new BasketItemNotFoundException("Ürün sepetinizde mevcut değil.");

            Items.Remove(item);

            AddEvent(new BasketItemRemovedEvent(CustomerId, itemId));
        }

        public void Clear()
        {
            if (Items.Count == 0) return;

            Items.Clear();

            AddEvent(new BasketCleanedEvent(CustomerId));
        }

        public bool AlreadyInBasketWithSameQuantity(int itemId, int quantity)
        {
            return Items.Any(x => x.ProductId == itemId && x.Quantity == quantity);
        }

        public bool AlreadyInBasketButDifferentQuantity(int itemId, int quantity)
        {
            return Items.Any(x => x.ProductId == itemId && x.Quantity != quantity);
        }
    }
}