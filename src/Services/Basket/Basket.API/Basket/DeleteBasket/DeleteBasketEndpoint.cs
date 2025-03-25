
using Basket.API.Basket.GetBasket;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketRequest(string UserName);
public record DeleteBasketResponse(bool isSuccess);


public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(userName));
            var response = result.Adapt<DeleteBasketResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("DeleteBasket")
        .WithDescription("Deletes a basket");
    }
}