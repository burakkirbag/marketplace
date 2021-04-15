using System.Threading;
using System.Threading.Tasks;
using Marketplace.Application.Commands;
using Marketplace.Baskets.Dto;
using Marketplace.Baskets.Exceptions;
using Marketplace.Baskets.Rules;
using Marketplace.Domain.Repositories;
using Marketplace.Mapper;
using MediatR;

namespace Marketplace.Baskets.Commands.ClearBasket
{
    public class ClearBasketCommandHandler : ICommandHandler<ClearBasketCommand>
    {
        private readonly IRepository<Basket> _basketRepository;

        public ClearBasketCommandHandler(IRepository<Basket> basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Unit> Handle(ClearBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId);
            if (basket == null) throw new BasketNotFoundException("Sepetinizde zaten ürün bulunmuyor.");

            basket.Clear();

            await _basketRepository.DeleteAsync(x => x.CustomerId == request.CustomerId);

            return Unit.Task.Result;
        }
    }
}