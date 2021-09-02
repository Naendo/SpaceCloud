using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Coworking.Domain.Entities
{
    
    public class Order : BaseEntity
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public string OrderNr { get; set; }

        public bool IsAccepted { get; set; }
        public bool IsDeclined { get; set; }
        public bool IsPayed { get; set; }


        [NotMapped]
        public string Status
        {
            get
            {
                if (IsDeclined)
                    return "declined";
                if (IsPayed && IsAccepted)
                    return "paid";
                if (IsAccepted)
                    return "billed";
                return "pending";
            }
        }


        public Invoice? Invoice { get; set; }
        public ICollection<Cart> CartItems { get; set; }

        /// <summary>
        ///     Required .Include(x => CartItems).ThenInclude(Product)
        /// </summary>
        public decimal GetPrice() =>
            CartItems.Sum(x => x.Product.Price);
    }
}