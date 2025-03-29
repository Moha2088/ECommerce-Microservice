using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.EntityTypeConfigurations
{
    class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.Id).HasConversion(
                orderId => orderId.Value,
                dbId => OrderId.Of(dbId));


            #region ValueObjectMapping

            builder.ComplexProperty(x => x.OrderName, nameBuilder =>
            {
                nameBuilder.Property(x => x.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });

            builder.ComplexProperty(x => x.ShippingAddress, shippingAddressBuilder =>
            {
                shippingAddressBuilder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                shippingAddressBuilder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

                shippingAddressBuilder.Property(x => x.EmailAddress)
                .HasMaxLength(50);

                shippingAddressBuilder.Property(x => x.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                shippingAddressBuilder.Property(x => x.Country)
                .HasMaxLength(50);

                shippingAddressBuilder.Property(x => x.State)
                .HasMaxLength(50);

                shippingAddressBuilder.Property(x => x.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
            });

            builder.ComplexProperty(x => x.BillingAddress, billingAddressBuilder =>
            {
                billingAddressBuilder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                billingAddressBuilder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

                billingAddressBuilder.Property(x => x.EmailAddress)
                .HasMaxLength(50);

                billingAddressBuilder.Property(x => x.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                billingAddressBuilder.Property(x => x.Country)
                .HasMaxLength(50);

                billingAddressBuilder.Property(x => x.State)
                .HasMaxLength(50);

                billingAddressBuilder.Property(x => x.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
            });

            builder.ComplexProperty(x => x.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(x => x.CardName)
                .HasMaxLength(50)
                .IsRequired();

                paymentBuilder.Property(x => x.CardNumber)
                .HasMaxLength(24)
                .IsRequired();

                paymentBuilder.Property(x => x.Expiration)
                .HasMaxLength(10)
                .IsRequired();

                paymentBuilder.Property(x => x.CVV)
                .HasMaxLength(3);

                paymentBuilder.Property(x => x.PaymentMethod);
            });
            #endregion

            builder.Property(x => x.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                x => x.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(x => x.TotalPrice);


            #region Relations
            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder.HasMany(x => x.OrderItems)
                .WithOne()
                .HasForeignKey(x => x.OrderId);
            #endregion
        }
    }
}
