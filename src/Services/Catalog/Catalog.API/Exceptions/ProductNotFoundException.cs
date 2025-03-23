
namespace Catalog.API.Exceptions;

[Serializable]
public class ProductNotFoundException : Exception
{
    public ProductNotFoundException() : base("Product not found!") { }
}
