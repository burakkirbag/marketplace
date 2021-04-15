using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Marketplace.Baskets;
using Marketplace.Baskets.Commands.AddItemToBasket;
using Marketplace.Baskets.Commands.ChangeQuantityOfBasketItem;
using Marketplace.Baskets.Rules;
using Marketplace.Domain.Repositories;
using Marketplace.Test.Core;
using Moq;
using Xunit;

namespace Marketplace.Application.BasketServices.Tests.Commands
{
    public class BasketCommandHandlerTests : UnitTestBase
    {
        [Fact]
        public async Task Basket_Should_Be_Created_With_Items()
        {
            var customerId = Faker.Random.Int(min: 1);
            var itemId = Faker.Random.Int(min: 1);
            var quantity = Faker.Random.Int(min: 1);

            var command = new AddItemToBasketCommand(customerId, itemId, quantity);

            var basketRepository = new Mock<IRepository<Basket>>();
            var itemStockChecker = new Mock<IItemStockChecker>();
            itemStockChecker.Setup(mr => mr.IsAvaliable(itemId, quantity)).Returns(Task.FromResult(true));

            var handler =
                new AddItemToBasketCommandHandler(basketRepository.Object, itemStockChecker.Object);

            var exception =
                await Record.ExceptionAsync(async () => await handler.Handle(command, new CancellationToken()));

            exception.Should().BeNull();
        }
    }
}