using System;
using Marketplace.Domain.Rules;

namespace Marketplace.Baskets.Rules
{
    public class ItemStockMustBeAvailableRule : IBusinessRule
    {
        private readonly IItemStockChecker _itemStockChecker;
        private readonly int _itemId;
        private readonly int _quantity;

        public ItemStockMustBeAvailableRule(IItemStockChecker itemStockChecker, int itemId,
            int quantity)
        {
            _itemStockChecker = itemStockChecker;
            _itemId = itemId;
            _quantity = quantity;
        }

        public string Message => "Sepete eklemek istediğiniz ürün için yeterli stok bulunmamaktadır.";

        public bool IsBroken()
        {
            return !_itemStockChecker.IsAvaliable(_itemId, _quantity).Result;
        }
    }
}