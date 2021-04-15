using AutoMapper;
using Marketplace.Baskets.Dto;
using Marketplace.Mapper;

namespace Marketplace.Baskets
{
    public class BasketServicesMapperConfiguration : Profile, IOrderedMapperProfile
    {
        public BasketServicesMapperConfiguration()
        {
            CreateBasketMaps();
        }

        private void CreateBasketMaps()
        {
            CreateMap<BasketItem, BasketItemDto>();
            CreateMap<Basket, BasketDto>();
        }

        public int Order => 0;
    }
}