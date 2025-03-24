

namespace Catalog.API.Products.DeleteProject;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required!");
    }
}

internal class DeleteProductCommandHandler(IDocumentSession documentSession) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await documentSession.LoadAsync<Product>(command.Id);

        if(product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        documentSession.Delete(product);
        await documentSession.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}