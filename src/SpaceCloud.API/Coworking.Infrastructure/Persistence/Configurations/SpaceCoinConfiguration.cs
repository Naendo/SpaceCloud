using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coworking.Infrastructure.Persistence.Configurations
{
    public class SpaceCoinConfiguration : IEntityTypeConfiguration<SpaceCoin>
    {
        public void Configure(EntityTypeBuilder<SpaceCoin> builder)
        {
            builder.HasKey(x => x.CoinId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.SpaceCoins)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}