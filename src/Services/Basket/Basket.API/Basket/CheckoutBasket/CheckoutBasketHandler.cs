﻿using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);


public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto)
            .NotNull()
            .WithMessage("BasketCheckoutDto can't be empty");

        RuleFor(x => x.BasketCheckoutDto.UserName)
            .NotEmpty()
            .WithMessage("Username can't be empty");
    }
}

public class CheckoutBasketHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName);

        if(basket == null)
        {
            return new CheckoutBasketResult(false);
        }

        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;
        await publishEndpoint.Publish(eventMessage, cancellationToken);
        await repository.DeleteBasket(basket.UserName, cancellationToken);
        return new CheckoutBasketResult(true);
    }
}