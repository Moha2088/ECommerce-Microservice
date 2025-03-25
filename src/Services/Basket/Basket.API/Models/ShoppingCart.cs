namespace Basket.API.Models;

public class ShoppingCart
{
    public string UserName { get; set; } = null!;
    
    public List<ShoppingCartItem> Items { get; set; } = new();

    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public ShoppingCart(string username)
    {
        UserName = username;
    }

    public ShoppingCart() { } // Required for mapping
}
