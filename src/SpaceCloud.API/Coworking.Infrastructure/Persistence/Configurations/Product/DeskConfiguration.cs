using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coworking.Infrastructure.Persistence.Configurations
{
    public class DeskConfiguration : IEntityTypeConfiguration<Desk>
    {
        public void Configure(EntityTypeBuilder<Desk> builder)
        {
            builder.HasKey(x => x.DeskId);

            builder.HasOne(x => x.Product)
                .WithOne(x => x.Desk)
                .HasForeignKey<Desk>(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}