

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCart)
            .NotNull()
            .WithMessage("Cart can't be null!");

        RuleFor(x => x.ShoppingCart.UserName)
            .NotEmpty()
            .WithMessage("Name is required!");
    }
}

internal class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var cart = command.ShoppingCart;
        return new StoreBasketResult(UserName: "");
    }
}
