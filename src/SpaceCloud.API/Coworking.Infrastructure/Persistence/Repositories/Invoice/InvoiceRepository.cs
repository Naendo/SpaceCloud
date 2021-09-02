using System.Linq;
using System.Threading.Tasks;
using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure.Persistence
{
    public class InvoiceRepository : BaseRepository<Invoice>
    {
        public InvoiceRepository(WorkingContext context) : base(context)
        {
        }

        public async Task UpdateOrder(Order order)
        {
            Context.Orders.Update(order);
            await Context.SaveChangesAsync();
        }


        public async Task<Order?> FindOrderByOrderIdAsync(int orderId)
        {
            return await Context.Orders
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .Include(x => x.User)
                .ThenInclude(x => x.Identity)
                .Include(x => x.User.SpaceCoins)
                .FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        public async Task<string> GetCompanyNameByCompanyIdAsync(int companyId)
        {
            var result = await Context.Companies.FirstOrDefaultAsync(x => x.CompanyId == companyId);
            return result.Name;
        }
        
        public async Task<int> GetAmountOfSubscriptionInOrder(Order order)
        {
            var productIds = order.CartItems.Select(x => x.ProductId);


            var counter = 0;

            foreach (var id in productIds)
            {
                var result = await Context.Desks.FirstOrDefaultAsync(x => x.ProductId == id);

                if (result is null)
                    continue;
                ++counter;
            }

            return counter;
        }
        
        public async Task AddCoin(SpaceCoin coins)
        {
            Context.SpaceCoins.Add(coins);
            await Context.SaveChangesAsync();
        }
    }
}