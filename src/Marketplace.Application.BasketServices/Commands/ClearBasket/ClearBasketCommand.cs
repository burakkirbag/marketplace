using System;
using Marketplace.Application.Commands;
using Marketplace.Baskets.Dto;

namespace Marketplace.Baskets.Commands.ClearBasket
{
    public class ClearBasketCommand : CommandBase
    {
        public readonly int CustomerId;

        public ClearBasketCommand(int customerId)
        {
            CustomerId = customerId;
        }
    }
}