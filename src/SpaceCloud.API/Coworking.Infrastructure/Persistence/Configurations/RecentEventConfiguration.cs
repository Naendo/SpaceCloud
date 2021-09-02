using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coworking.Infrastructure.Persistence.Configurations
{
    public class RecentEventConfiguration : IEntityTypeConfiguration<RecentEvent>
    {
        public void Configure(EntityTypeBuilder<RecentEvent> builder)
        {
            builder.HasKey(x => x.ActionId);
            builder.HasOne(x => x.User).WithMany(x => x.Events)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}