using System;
using Marketplace.Application.Commands;
using Marketplace.Baskets.Dto;

namespace Marketplace.Baskets.Commands.ChangeQuantityOfBasketItem
{
    public class ChangeQuantityOfBasketItemCommand : CommandBase
    {
        public readonly int CustomerId;
        public readonly int ItemId;
        public readonly int Quantity;

        public ChangeQuantityOfBasketItemCommand(int customerId, int itemId, int quantity)
        {
            CustomerId = customerId;
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}