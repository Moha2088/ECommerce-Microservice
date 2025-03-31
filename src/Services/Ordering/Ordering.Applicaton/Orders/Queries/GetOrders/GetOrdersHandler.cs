using BuildingBlocks.Pagination;

namespace Ordering.Applicaton.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;
            var totalCount = await context.Orders.LongCountAsync();

            var orders = await context.Orders
                .AsNoTracking()
                .Include(x => x.OrderItems)
                .OrderBy(x => x.OrderName.Value)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetOrdersResult(
                new PaginatedResult<OrderDto>(pageIndex, pageSize, totalCount, orders.ToOrderDtoList()));
        }
    }
}