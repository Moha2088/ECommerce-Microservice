using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
        }

        dbContext.Coupon.Add(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Discount is successfully created. ProductName : {coupon.ProductName}");
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupon.FirstOrDefaultAsync(x => x.ProductName.Equals(request.ProductName));

        if (coupon is null)
        {
            coupon = new Coupon { ProductName = "No discount", Amount = 0, Description = "No discount" };
        }

        logger.LogInformation($"Discount is retrieved for ProductName : {coupon.ProductName}, Amount : {coupon.Amount}");
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
        }

        dbContext.Coupon.Update(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Discount has been updated. ProductName : {coupon.ProductName}");
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupon.FirstOrDefaultAsync(x => x.ProductName.Equals(request.ProductName));

        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
        }

        dbContext.Coupon.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation($"Discount has been deleted. ProductName : {request.ProductName}");

        return new DeleteDiscountResponse
        {
            Success = true
        };
            }
}