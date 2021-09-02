using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coworking.Domain.Entities;
using Coworking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(WorkingContext context) : base(context)
        {
        }


        public async Task<Product> ReadProductWithCartAsync(int productId)
        {
            return await Context.Products.Include(x => x.CartEntries).ThenInclude(x => x.Order)
                .FirstOrDefaultAsync(x => x.ProductId == productId);
        }


        public async Task<IEnumerable<Room>> ReadAllRoomsByLocationId(int locationId)
        {
            return await Context.Rooms.Include(x => x.Product)
                .Include(x => x.Tags)
                .Where(x => x.Product.LocationId == locationId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Desk>> ReadAllDesksByLocationId(int locationId)
        {
            return await Context.Desks.Include(x => x.Product)
                .Where(x => x.Product.LocationId == locationId)
                .ToListAsync();
        }


        public async Task<Room> AddRoomAsync(Room room)
        {
            Context.Rooms.Add(room);
            await Context.SaveChangesAsync();
            return room;
        }

        public async Task<Desk> AddDeskAsync(Desk desk)
        {
            Context.Desks.Add(desk);
            await Context.SaveChangesAsync();
            return desk;
        }

        public async Task<Desk?> ReadDeskByProductId(int productId)
        {
            var desk = await Context.Desks.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (desk is null)
                return null;
            
            var product = await Context.Products.Include(x => x.CartEntries)
                .ThenInclude(x => x.Order).FirstOrDefaultAsync(x => x.ProductId == productId);

            desk.Product = product;
            return desk;
        }

        public async Task<Room?> ReadRoomByProductId(int productId)
        {
            var room = await Context.Rooms.FirstOrDefaultAsync(x => x.ProductId == productId);

            if (room is null)
                return null;

            var product = await Context.Products.Include(x => x.CartEntries)
                .ThenInclude(x => x.Order).FirstOrDefaultAsync(x => x.ProductId == productId);

            room.Product = product;

            return room;
        }
    }
}