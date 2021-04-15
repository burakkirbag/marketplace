using System;
using System.Threading.Tasks;
using Marketplace.Api.Models.Requests;
using Marketplace.Api.Mvc.Controllers;
using Marketplace.Api.Mvc.Models;
using Marketplace.Baskets.Commands.AddItemToBasket;
using Marketplace.Baskets.Commands.ChangeQuantityOfBasketItem;
using Marketplace.Baskets.Commands.ClearBasket;
using Marketplace.Baskets.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers
{
    public class BasketController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Route("{customerId}")]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int customerId, [FromBody] AddItemToBasketRequest request)
        {
            if (customerId <= 0)
                return BadRequest("Müşteri bilgisini belirtmelisiniz.",
                    $"Müşteriye ait geçerli bir Id girmelisiniz. Müşteri Id değeri 0'dan büyük olmalıdır. customerId : {customerId}");

            await _mediator.Send(new AddItemToBasketCommand(customerId, request.ItemId, request.Quantity));

            return Success("Ürün sepetinize eklendi.",
                $"Ürün müşteri sepetine eklendi. customerId : {customerId} itemId : {request.ItemId}");
        }

        [HttpPut]
        [Route("{customerId}/items")]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int customerId, [FromBody] ChangeQuantityOfBasketItemRequest request)
        {
            if (customerId <= 0)
                return BadRequest("Müşteri bilgisini belirtmelisiniz.",
                    $"Müşteriye ait geçerli bir Id girmelisiniz. Müşteri Id değeri 0'dan büyük olmalıdır. customerId : {customerId}");

            await _mediator.Send(new ChangeQuantityOfBasketItemCommand(customerId, request.ItemId, request.Quantity));

            return Success("Sepetinizdeki ürün miktarı güncellendi.",
                $"Müşterinin sepetindeki ürün miktarı güncellendi. customerId : {customerId} itemId : {request.ItemId} quantity : {request.Quantity}");
        }

        [HttpDelete]
        [Route("{customerId}/clear")]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int customerId)
        {
            if (customerId <= 0)
                return BadRequest("Müşteri bilgisini belirtmelisiniz.",
                    $"Müşteriye ait geçerli bir Id girmelisiniz. Müşteri Id değeri 0'dan büyük olmalıdır. customerId : {customerId}");

            await _mediator.Send(new ClearBasketCommand(customerId));

            return Success("Sepetinizdeki ürünler silindi.",
                $"Müşterinin sepetindeki ürünler silindi. customerId : {customerId}");
        }
    }
}