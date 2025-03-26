using Microsoft.EntityFrameworkCore;
using Discount.gRPC.Models;

namespace Discount.gRPC.Data;

public class DiscountContext : DbContext
{
    public DiscountContext(DbContextOptions<DiscountContext> options) 
        :base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone smartphone", Amount = 1500 },
            new Coupon { Id = 2, ProductName = "Samsung S10", Description = "Samsung smartphone", Amount = 1000 }
            );
    }

    public DbSet<Coupon> Coupon { get; set; }
}
