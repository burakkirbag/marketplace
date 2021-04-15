using System;
using FluentValidation;
using Marketplace.Api.Models.Requests;

namespace Marketplace.Api.Validators
{
    public class ChangeQuantityOfBasketItemRequestValidator : AbstractValidator<ChangeQuantityOfBasketItemRequest>
    {
        public ChangeQuantityOfBasketItemRequestValidator()
        {
            RuleFor(x => x.ItemId)
                .NotNull()
                .WithMessage("Sepete eklemek istediğiniz ürünü belirtmelisiniz.")
                .NotEmpty()
                .WithMessage("Sepete eklemek istediğiniz ürünü belirtmelisiniz.")
                .GreaterThan(0)
                .WithMessage("Sepete eklemek istediğiniz ürünü belirtmelisiniz.");

            RuleFor(x => x.Quantity)
                .NotNull()
                .WithMessage("Sepete eklemek istediğiniz ürün miktarını belirtmelisiniz.")
                .NotEmpty()
                .WithMessage("Sepete eklemek istediğiniz ürün miktarını belirtmelisiniz.")
                .GreaterThan(0)
                .WithMessage("Sepete eklemek istediğiniz ürün miktarını belirtmelisiniz.");
        }
    }
}