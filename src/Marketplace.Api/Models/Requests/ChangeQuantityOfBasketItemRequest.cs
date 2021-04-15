using System;

namespace Marketplace.Api.Models.Requests
{
    public class ChangeQuantityOfBasketItemRequest
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}