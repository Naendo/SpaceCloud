using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coworking.Infrastructure.Persistence.Configurations
{
    public class RoomConfiguartion : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(x => x.RoomId);

            builder.HasOne(x => x.Product)
                .WithOne(x => x.Room)
                .HasForeignKey<Room>(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}