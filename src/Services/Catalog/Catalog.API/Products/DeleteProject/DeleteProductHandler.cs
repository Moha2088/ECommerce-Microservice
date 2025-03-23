

namespace Catalog.API.Products.DeleteProject;

public record DeleteProductCommand(Guid Id) :ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);



internal class DeleteProductCommandHandler(IDocumentSession documentSession) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await documentSession.LoadAsync<Product>(command.Id) ?? throw new ProductNotFoundException();
        documentSession.Delete(product);
        await documentSession.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}