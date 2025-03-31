using BuildingBlocks.Exceptions;

namespace Ordering.Applicaton.Exceptions;
public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid id) : base("Order", id)
    {
    }
}