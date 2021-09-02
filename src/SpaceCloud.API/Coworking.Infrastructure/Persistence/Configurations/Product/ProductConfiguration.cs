using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coworking.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.ProductId);

            builder.HasOne(x => x.LastModifiedBy).WithMany(x => x.Products)
                .HasForeignKey(x => x.LastModifiedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Location).WithMany(x => x.Products)
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}