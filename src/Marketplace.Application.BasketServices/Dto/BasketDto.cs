using System;
using System.Collections.Generic;
using Marketplace.Application.Dto;

namespace Marketplace.Baskets.Dto
{
    public class BasketDto
    {
        public int CustomerId { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}