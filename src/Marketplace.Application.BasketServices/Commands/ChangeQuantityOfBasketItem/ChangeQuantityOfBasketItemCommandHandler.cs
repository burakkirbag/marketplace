using System;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Application.Commands;
using Marketplace.Baskets.Dto;
using Marketplace.Baskets.Exceptions;
using Marketplace.Baskets.Rules;
using Marketplace.Domain.Repositories;
using Marketplace.Mapper;
using MediatR;

namespace Marketplace.Baskets.Commands.ChangeQuantityOfBasketItem
{
    public class
        ChangeQuantityOfBasketItemCommandHandler : ICommandHandler<ChangeQuantityOfBasketItemCommand>
    {
        private readonly IRepository<Basket> _basketRepository;
        private readonly IItemStockChecker _itemStockChecker;

        public ChangeQuantityOfBasketItemCommandHandler(IRepository<Basket> basketRepository,
            IItemStockChecker itemStockChecker)
        {
            _basketRepository = basketRepository;
            _itemStockChecker = itemStockChecker;
        }

        public async Task<Unit> Handle(ChangeQuantityOfBasketItemCommand request,
            CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId);
            if (basket == null) throw new BasketNotFoundException("Sepetiniz boş.");

            basket.ChangeItemQuantity(request.ItemId, request.Quantity, _itemStockChecker);

            await _basketRepository.UpdateAsync(basket);

            return Unit.Task.Result;
        }
    }
}