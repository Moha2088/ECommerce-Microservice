﻿

namespace Basket.API.Data;

public class BasketRepository(IDocumentSession documentSession) : IBasketRepository
{
    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        documentSession.Store(basket);
        await documentSession.SaveChangesAsync(cancellationToken);
        return basket;
    }

    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await documentSession.LoadAsync<ShoppingCart>(userName, cancellationToken);
        return basket is not null ? basket : throw new BasketNotFoundException(userName);
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        documentSession.Delete<ShoppingCart>(userName);
        await documentSession.SaveChangesAsync();
        return true;
    }
}
