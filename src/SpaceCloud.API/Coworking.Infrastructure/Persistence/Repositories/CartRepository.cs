using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure.Persistence
{
    public class CartRepository : BaseRepository<Cart>
    {
        public CartRepository(WorkingContext context) : base(context)
        {
        }

        public async Task<List<Cart>> ReadAllCartsByProductId(int productId)
        {
            return await Context.Carts
                .Where(x => x.ProductId == productId && x.EndDate >= DateTime.Now && x.StartDate <= DateTime.Now)
                .ToListAsync();
        }
    }
}