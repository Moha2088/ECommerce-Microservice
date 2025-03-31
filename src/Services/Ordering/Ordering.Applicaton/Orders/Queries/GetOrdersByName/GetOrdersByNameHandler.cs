namespace Ordering.Applicaton.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await context.Orders
                .AsNoTracking()
                .Include(x => x.OrderItems)
                .Where(x => x.OrderName.Value.Contains(query.Name))
                .OrderBy(x => x.OrderName.Value)
                .ToListAsync(cancellationToken);


            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }
    }
}