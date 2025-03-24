
namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);


public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required!");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Category is required!");

        RuleFor(x => x.ImageFile)
            .NotEmpty()
            .WithMessage("Imagefile is required!");

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price is required!");
    }
}
    

internal class CreateProductCommandHandler(IDocumentSession documentSession) : 
    ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            ImageFile = command.ImageFile,
            Category = command.Category,
            Description = command.Description,
            Price = command.Price
        };

        documentSession.Store(product);
        await documentSession.SaveChangesAsync();

        return new CreateProductResult(product.Id);
    }
}