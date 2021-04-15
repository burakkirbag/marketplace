using System;
using System.Collections.Generic;
using Marketplace.Domain.Values;

namespace Marketplace.Baskets
{
    public class BasketItem : ValueObjectBase
    {
        public virtual int ProductId { get; protected set; }
        public virtual int Quantity { get; protected set; }

        public BasketItem(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public static BasketItem Create(int productId, int quantity)
        {
            var item = new BasketItem(productId, quantity);

            return item;
        }

        public void ChangeQuantity(int quantity)
        {
            Quantity = quantity;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return ProductId;
            yield return Quantity;
        }
    }
}