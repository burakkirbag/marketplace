using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using FluentAssertions;
using Marketplace.Api.IntegrationTests.Extensions;
using Marketplace.Api.Models.Requests;
using Marketplace.Test.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Marketplace.Api.IntegrationTests.Controllers
{
    public class BasketControllerTests : IntegrationTestBase<Startup>
    {
        public BasketControllerTests() : base()
        {
            Host.Start();
        }

        [Fact]
        public async Task Add_Item_To_Basket_Should_Return_Success()
        {
            var customerId = 5;
            var itemId = 1;
            var quantity = 2;
            var request = new AddItemToBasketRequest {ItemId = itemId, Quantity = quantity};

            var response = await Client.PostAsync($"/api/v1/basket/{customerId}", request.ToJsonStringContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Add_Item_To_Basket_Should_Return_BadRequest_When_Provided_Request_Does_Not_Proper()
        {
            var customerId = 5;
            var itemId = 0;
            var quantity = 2;
            var request = new AddItemToBasketRequest {Quantity = quantity, ItemId = itemId};

            var response = await Client.PostAsync($"/api/v1/basket/{customerId}", request.ToJsonStringContent());

            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Add_Item_To_Basket_With_Zero_Quantity_Should_Return_BadRequest()
        {
            var customerId = 5;
            var itemId = 1;
            var quantity = 0;
            var request = new AddItemToBasketRequest {Quantity = quantity, ItemId = itemId};

            var response = await Client.PostAsync($"/api/v1/basket/{customerId}", request.ToJsonStringContent());

            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Change_Quantity_Of_Item_Should_Return_Success()
        {
            var customerId = 5;
            var itemId = 1;
            const int quantity = 6;
            var request = new ChangeQuantityOfBasketItemRequest() {Quantity = quantity, ItemId = itemId};

            var response = await Client.PutAsync($"/api/v1/basket/{customerId}/items", request.ToJsonStringContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Change_Quantity_Of_Item_Should_Return_NotFound_When_Basket_Does_Not_Exist()
        {
            var customerId = 5;
            var itemId = 7;
            var quantity = 1;
            var request = new ChangeQuantityOfBasketItemRequest() {Quantity = quantity, ItemId = itemId};

            var response = await Client.PutAsync($"/api/v1/basket/{customerId}/items", request.ToJsonStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Clear_Basket_Should_Return_Success()
        {
            var customerId = 5;

            var response = await Client.DeleteAsync($"/api/v1/basket/{customerId}/clear");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Clear_Basket_Should_Return_Not_Found_When_Basket_Does_Not_Exist()
        {
            var customerId = 9999;

            var response = await Client.DeleteAsync($"/api/v1/basket/{customerId}/clear");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}