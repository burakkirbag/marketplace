using System;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Application.Commands;
using Marketplace.Baskets.Dto;
using Marketplace.Baskets.Rules;
using Marketplace.Domain.Repositories;
using Marketplace.Mapper;
using MediatR;

namespace Marketplace.Baskets.Commands.AddItemToBasket
{
    public class AddItemToBasketCommandHandler : ICommandHandler<AddItemToBasketCommand>
    {
        private readonly IRepository<Basket> _basketRepository;
        private readonly IItemStockChecker _itemStockChecker;

        public AddItemToBasketCommandHandler(IRepository<Basket> basketRepository, IItemStockChecker itemStockChecker)
        {
            _basketRepository = basketRepository;
            _itemStockChecker = itemStockChecker;
        }

        public async Task<Unit> Handle(AddItemToBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId);
            if (basket == null) basket = Basket.Create(request.CustomerId);

            basket.AddItem(request.ItemId, request.Quantity, _itemStockChecker);

            await _basketRepository.InsertOrUpdateAsync(basket);

            return Unit.Task.Result;
        }
    }
}