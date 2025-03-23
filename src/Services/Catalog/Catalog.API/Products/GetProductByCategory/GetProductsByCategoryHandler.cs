

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);


internal class GetProductsByCategoryHandler(IDocumentSession documentSession) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await documentSession.Query<Product>()    
            .Where(x => x.Category.Contains(query.Category))
            .ToListAsync(cancellationToken) ?? throw new ProductNotFoundException();

        return new GetProductsByCategoryResult(products);
    }
}