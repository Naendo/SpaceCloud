using System.Reflection;
using Coworking.Domain;
using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure.Persistence
{
    public class WorkingContext : DbContext
    {
        public WorkingContext(DbContextOptions<WorkingContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyLocation> CompanyLocations { get; set; }
        public DbSet<BillingAddress> BillingAddresses { get; set; }
        public DbSet<CompanySettings> CompanySettings { get; set; }
        public DbSet<Identity> Identities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<RecentEvent> RecentEvents { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomTags> RoomTags { get; set; }

        public DbSet<SpaceCoin> SpaceCoins { get; set; }


        public DbSet<Scheduler> Schedulers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}