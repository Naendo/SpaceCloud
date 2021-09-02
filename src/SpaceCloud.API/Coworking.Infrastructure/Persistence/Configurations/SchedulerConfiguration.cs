using Coworking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coworking.Infrastructure.Persistence.Configurations
{
    public class SchedulerConfiguration : IEntityTypeConfiguration<Scheduler>
    {
        public void Configure(EntityTypeBuilder<Scheduler> builder)
        {
            builder.HasKey(x => x.SchedulerId);

            builder.HasOne(x => x.Room)
                .WithOne(x => x.Scheduler)
                .HasForeignKey<Scheduler>(x => x.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Schedulers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}