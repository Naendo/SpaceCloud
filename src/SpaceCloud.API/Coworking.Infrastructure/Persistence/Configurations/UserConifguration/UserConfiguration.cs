using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coworking.Infrastructure.Persistence.Configurations
{
    public class UserConifuration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.HasOne(x => x.Identity).WithOne(x => x.User)
                .HasForeignKey<User>(x => x.IdentityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.BillingAddress).WithOne(x => x.User)
                .HasForeignKey<User>(x => x.BillingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Location).WithMany(x => x.Users)
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}