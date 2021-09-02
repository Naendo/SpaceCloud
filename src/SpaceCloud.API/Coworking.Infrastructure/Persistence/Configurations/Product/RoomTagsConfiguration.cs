using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coworking.Infrastructure.Persistence.Configurations
{
    public class RoomTagsConfiguration : IEntityTypeConfiguration<RoomTags>
    {
        public void Configure(EntityTypeBuilder<RoomTags> builder)
        {
            builder.HasKey(x => x.TagId);

            builder.HasOne(x => x.Room).WithMany(x => x.Tags)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}