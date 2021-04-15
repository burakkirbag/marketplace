using System;
using Marketplace.Application.Commands;
using Marketplace.Baskets.Dto;

namespace Marketplace.Baskets.Commands.AddItemToBasket
{
    public class AddItemToBasketCommand : CommandBase
    {
        public readonly int CustomerId;
        public readonly int ItemId;
        public readonly int Quantity;

        public AddItemToBasketCommand(int customerId, int itemId, int quantity)
        {
            CustomerId = customerId;
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}