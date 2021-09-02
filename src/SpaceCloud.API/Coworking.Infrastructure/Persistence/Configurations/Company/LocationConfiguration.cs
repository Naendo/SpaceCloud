using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coworking.Infrastructure.Persistence.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<CompanyLocation>
    {
        public void Configure(EntityTypeBuilder<CompanyLocation> builder)
        {
            builder.HasKey(x => x.LocationId);

            builder.HasOne(x => x.Company)
                .WithMany(x => x.Locations)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}