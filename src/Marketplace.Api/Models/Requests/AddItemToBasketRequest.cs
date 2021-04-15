using System;

namespace Marketplace.Api.Models.Requests
{
    public class AddItemToBasketRequest
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}