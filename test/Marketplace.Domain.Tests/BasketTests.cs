using System.Threading.Tasks;
using FluentAssertions;
using Marketplace.Baskets;
using Marketplace.Baskets.Events;
using Marketplace.Baskets.Exceptions;
using Marketplace.Baskets.Rules;
using Marketplace.Test.Core;
using Moq;
using Xunit;

namespace Marketplace.Domain.Tests
{
    public class BasketTests : UnitTestBase
    {
        [Fact]
        public void Basket_Should_Be_Created_Without_Any_Item()
        {
            var customerId = Faker.Random.Int(min: 1);
            var basketCreatedEvent = new BasketCreatedEvent(customerId);

            var basket = Basket.Create(customerId);

            basket.ShouldPublishDomainEvents(basketCreatedEvent);
        }

        [Fact]
        public void BasketItem_Should_Be_Added_To_Basket_When_Item_Quantity_Greater_Than_Zero()
        {
            var customerId = Faker.Random.Int(min: 1);
            var itemId = Faker.Random.Int(min: 1);
            var quantity = 2;

            var itemStockChecker = new Mock<IItemStockChecker>();
            itemStockChecker.Setup(mr => mr.IsAvaliable(itemId, quantity)).Returns(Task.FromResult(true));

            var basketCreatedEvent = new BasketCreatedEvent(customerId);
            var basketItemAddedEvent = new BasketItemAddedEvent(customerId, itemId, quantity);

            var basket = Basket.Create(customerId);
            basket.AddItem(itemId, quantity, itemStockChecker.Object);

            basket.Items.Count.Should().Be(1);
            basket.ShouldPublishDomainEvents(basketCreatedEvent, basketItemAddedEvent);
        }

        [Fact]
        public void Adding_SameItem_With_Same_Quantity_To_Basket_Should_Be_Idempotent()
        {
            var customerId = Faker.Random.Int(min: 1);
            var itemId = Faker.Random.Int(min: 1);
            var quantity = 2;

            var itemStockChecker = new Mock<IItemStockChecker>();
            itemStockChecker.Setup(mr => mr.IsAvaliable(itemId, quantity)).Returns(Task.FromResult(true));

            var basketCreatedEvent = new BasketCreatedEvent(customerId);
            var basketItemAddedEvent = new BasketItemAddedEvent(customerId, itemId, quantity);

            var basket = Basket.Create(customerId);
            basket.AddItem(itemId, quantity, itemStockChecker.Object);
            basket.AddItem(itemId, quantity, itemStockChecker.Object);

            basket.Items.Count.Should().Be(1);
            basket.ShouldPublishDomainEvents(basketCreatedEvent, basketItemAddedEvent);
        }

        [Fact]
        public void Adding_SameItem_With_Different_Quantity_To_Basket_Should_Change_Quantity()
        {
            var customerId = Faker.Random.Int(min: 1);
            var itemId = Faker.Random.Int(min: 1);
            var quantity = 2;
            var toQuantity = 4;

            var itemStockChecker = new Mock<IItemStockChecker>();
            itemStockChecker.Setup(mr => mr.IsAvaliable(itemId, quantity)).Returns(Task.FromResult(true));
            itemStockChecker.Setup(mr => mr.IsAvaliable(itemId, toQuantity)).Returns(Task.FromResult(true));

            var basketCreatedEvent = new BasketCreatedEvent(customerId);
            var basketItemAddedEvent = new BasketItemAddedEvent(customerId, itemId, quantity);
            var basketItemQuantityChangedEvent =
                new BasketItemQuantityChangedEvent(customerId, itemId, quantity, toQuantity);

            var basket = Basket.Create(customerId);
            basket.AddItem(itemId, quantity, itemStockChecker.Object);
            basket.AddItem(itemId, toQuantity, itemStockChecker.Object);

            basket.Items.Count.Should().Be(1);
            basket.ShouldPublishDomainEvents(basketCreatedEvent, basketItemAddedEvent, basketItemQuantityChangedEvent);
        }

        [Fact]
        public async Task Changing_Not_Existed_Item_Quantity_Should_Throw_BasketItemNotFoundException()
        {
            var customerId = Faker.Random.Int(min: 1);
            var itemId = Faker.Random.Int(min: 1);
            var notExistedItemId = Faker.Random.Int(min: 1);
            var quantity = 2;

            var itemStockChecker = new Mock<IItemStockChecker>();
            itemStockChecker.Setup(mr => mr.IsAvaliable(itemId, quantity)).Returns(Task.FromResult(true));
            itemStockChecker.Setup(mr => mr.IsAvaliable(notExistedItemId, quantity)).Returns(Task.FromResult(true));

            var basket = Basket.Create(customerId);
            basket.AddItem(itemId, quantity, itemStockChecker.Object);

            var exception =
                await Record.ExceptionAsync(async () =>
                    basket.ChangeItemQuantity(notExistedItemId, quantity, itemStockChecker.Object));

            exception.Should().NotBeNull();
            exception.GetType().Equals(typeof(BasketItemNotFoundException));
        }

        [Fact]
        public async Task
            BasketItem_Should_Not_Be_Added_When_Item_Quantity_Is_Less_or_Equal_To_Zero_And_Should_Throw_BasketInvalidItemQuantityException()
        {
            var customerId = Faker.Random.Int(min: 1);
            var itemId = Faker.Random.Int(min: 1);
            var quantity = 0;

            var itemStockChecker = new Mock<IItemStockChecker>();
            itemStockChecker.Setup(mr => mr.IsAvaliable(itemId, quantity)).Returns(Task.FromResult(true));

            var basket = Basket.Create(customerId);

            var exception =
                Record.ExceptionAsync(async () => basket.AddItem(itemId, quantity, itemStockChecker.Object));

            exception.Should().NotBeNull();
            exception.GetType().Equals(typeof(BasketInvalidItemQuantityException));
        }
        
        [Fact]
        public void BasketItem_Quantity_Should_Be_Changed()
        {
            var customerId = Faker.Random.Int(min: 1);
            var itemId = Faker.Random.Int(min: 1);
            const int fromQuantity = 2;
            const int toQuantity = 4;

            var itemStockChecker = new Mock<IItemStockChecker>();
            itemStockChecker.Setup(mr => mr.IsAvaliable(itemId, fromQuantity)).Returns(Task.FromResult(true));
            itemStockChecker.Setup(mr => mr.IsAvaliable(itemId, toQuantity)).Returns(Task.FromResult(true));

            var basketCreatedEvent = new BasketCreatedEvent(customerId);
            var basketItemAddedEvent = new BasketItemAddedEvent(customerId, itemId, fromQuantity);
            var basketItemQuantityChangedEvent =
                new BasketItemQuantityChangedEvent(customerId, itemId, fromQuantity, toQuantity);

            var basket = Basket.Create(customerId);
            basket.AddItem(itemId, fromQuantity, itemStockChecker.Object);
            basket.ChangeItemQuantity(itemId, toQuantity, itemStockChecker.Object);

            basket.Items.Count.Should().Be(1);
            basket.ShouldPublishDomainEvents(basketCreatedEvent, basketItemAddedEvent, basketItemQuantityChangedEvent);
        }
        
        [Fact]
        public void BasketItem_Quantity_Should_Be_Changed_And_Product_Should_Be_Removed_When_Quantity_Is_Zero()
        {
            var customerId = Faker.Random.Int(min: 1);
            var itemId = Faker.Random.Int(min: 1);
            const int fromQuantity = 2;
            const int toQuantity = 0;

            var itemStockChecker = new Mock<IItemStockChecker>();
            itemStockChecker.Setup(mr => mr.IsAvaliable(itemId, fromQuantity)).Returns(Task.FromResult(true));
            itemStockChecker.Setup(mr => mr.IsAvaliable(itemId, toQuantity)).Returns(Task.FromResult(true));

            var basketCreatedEvent = new BasketCreatedEvent(customerId);
            var basketItemAddedEvent = new BasketItemAddedEvent(customerId, itemId, fromQuantity);
            var basketItemRemovedEvent = new BasketItemRemovedEvent(customerId, itemId);

            var basket = Basket.Create(customerId);
            basket.AddItem(itemId, fromQuantity, itemStockChecker.Object);
            basket.ChangeItemQuantity(itemId, toQuantity, itemStockChecker.Object);

            basket.Items.Count.Should().Be(0);
            basket.ShouldPublishDomainEvents(basketCreatedEvent, basketItemAddedEvent, basketItemRemovedEvent);
        }
        
        [Fact]
        public void Clearing_Items_From_Basket_Should_Clear_All_Items_In_The_Basket()
        {
            var customerId = Faker.Random.Int(min: 1);
            var itemId = Faker.Random.Int(min: 1);
            const int fromQuantity = 2;

            var itemStockChecker = new Mock<IItemStockChecker>();
            itemStockChecker.Setup(mr => mr.IsAvaliable(itemId, fromQuantity)).Returns(Task.FromResult(true));
            
            var basketCreatedEvent = new BasketCreatedEvent(customerId);
            var basketItemAddedEvent = new BasketItemAddedEvent(customerId, itemId, fromQuantity);
            var basketCleanedEvent = new BasketCleanedEvent(customerId);

            var basket = Basket.Create(customerId);
            basket.AddItem(itemId, fromQuantity, itemStockChecker.Object);
            basket.Clear();

            basket.Items.Count.Should().Be(0);
            basket.ShouldPublishDomainEvents(basketCreatedEvent, basketItemAddedEvent, basketCleanedEvent);
        }
        
        [Fact]
        public void Clearing_Items_From_Basket_Should_Be_Idempotent()
        {
            var customerId = Faker.Random.Int(min: 1);
            var itemId = Faker.Random.Int(min: 1);
            
            var basketCreatedEvent = new BasketCreatedEvent(customerId);

            var basket = Basket.Create(customerId);
            basket.Clear();

            basket.Items.Count.Should().Be(0);
            basket.ShouldPublishDomainEvents(basketCreatedEvent);
        }
    }
}