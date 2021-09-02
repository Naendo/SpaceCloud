using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coworking.Domain;
using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure.Persistence
{
    public class SchedulerRepository : BaseRepository<Scheduler>
    {
        public SchedulerRepository(WorkingContext context) : base(context)
        {
        }

        public async Task<Room> ReadSchedulerAsync(int productId)
        {
            var result = await Context.Carts
                .Include(x => x.Product)
                .ThenInclude(x => x.Room)
                .Include(x => x.Order)
                .Include(x => x.Product.LastModifiedBy)
                .Where(x => x.CreatedAt.DayOfYear == DateTime.Now.DayOfYear)
                .Select(x => x.Product.Room)
                .FirstOrDefaultAsync(x => x.ProductId == productId);
            return result;
        }

        public async Task<Scheduler> FindSchedulerByActivator(string token)
        {
            return await Context.Schedulers
                .Include(x => x.Room)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.CartEntries)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.ActivatorToken == token);
        }

        public async Task<List<Cart>> ReadAllCartEntriesForCurrentDateByScheduler(Scheduler scheduler)
        {
            return await Context.Carts
                .Where(x => x.ProductId == scheduler.Room.ProductId)
                .Where(x => x.StartDate.DayOfYear == DateTime.Now.DayOfYear &&
                            x.StartDate.Year == DateTime.Now.Year)
                .ToListAsync();
        }


        public async Task<Scheduler> GetSchedulerWithProduct(int schedulerId)
        {
            return await Context.Schedulers
                .Include(x => x.User)
                .Include(x => x.Room)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.SchedulerId == schedulerId);
        }


        public async Task<Scheduler?> FindSchedulerByReferenceToken(string token)
        {
            return await Context.Schedulers.FirstOrDefaultAsync(x => x.ReferenceToken == token);
        }
    }
}