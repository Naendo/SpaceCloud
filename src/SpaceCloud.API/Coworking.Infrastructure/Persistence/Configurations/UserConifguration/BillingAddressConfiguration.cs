using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coworking.Infrastructure.Persistence.Configurations
{
    public class BillingAddressConfiguration : IEntityTypeConfiguration<BillingAddress>
    {
        public void Configure(EntityTypeBuilder<BillingAddress> builder)
        {
            builder.HasKey(x => x.BillingId);
        }
    }
}