using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coworking.Infrastructure.Persistence.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.CompanyId);

            builder.HasOne(x => x.Settings)
                .WithOne(x => x.Company)
                .HasForeignKey<Company>(x => x.SettingsId);

            builder.HasIndex(x => x.SubDomain)
                .IsUnique();
        }
    }
}