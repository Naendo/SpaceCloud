using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coworking.Domain.Entities;
using Coworking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(WorkingContext context) : base(context)
        {
        }


        public async Task AppendCartToOrderAsync(IEnumerable<Cart> carts)
        {
            await Context.Carts.AddRangeAsync(carts);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllOrdersByLocationId(int locationId)
        {
            return await Context.Orders
                .Include(x => x.User)
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .Where(x => x.User.LocationId == locationId)
                .ToListAsync();
        }

        /// <summary>
        ///     For order/add should never fail, just security for third party callers. Should already be prevented in frontend
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newEntries"></param>
        /// <returns></returns>
        public async Task<List<Cart>> GetListOfEntriesExistingBetweenTimeSpan(int productId, List<Cart> newEntries)
        {
            var cartEntries = (await Context.Products.FirstOrDefaultAsync(x => x.ProductId == productId)).CartEntries;

            var list = new List<Cart>();

            foreach (var entry in cartEntries)
            foreach (var newEntry in newEntries)
                //https://stackoverflow.com/questions/13513932/algorithm-to-detect-overlapping-periods
                if (entry.StartDate < newEntry.EndDate && newEntry.StartDate < newEntry.EndDate)
                    list.Add(entry);

            return list;
        }


        public async Task<Order> FindOrderIncludeUserAsync(int orderId)
        {
            return await Context.Orders
                .Include(x => x.Invoice)
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .Include(x => x.User).FirstOrDefaultAsync(x => x.OrderId == orderId);
        }


        public async Task<List<Order>> GetOrdersByUserId(int userId)
        {
            return await Context.Orders.Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}