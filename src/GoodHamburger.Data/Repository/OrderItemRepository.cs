using GoodHamburger.Business.Interfaces;
using GoodHamburger.Business.Models;
using GoodHamburger.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GoodHamburger.Data.Repository
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(GHContext context) : base(context)
        {
        }

        public async Task<OrderItem> SearchIdOrderItem(int id)
        {
            return await _ghcontext.OrderItems.AsNoTracking()
           .FirstOrDefaultAsync(OI => OI.Id == id);
            
        }
        public async Task<IEnumerable<OrderItem>> SearchIdOrderItemlist(int id)
        {
            return await _ghcontext.OrderItems.AsNoTracking()
                .Where(OI=>OI.OrderId == id)
                .OrderBy(OI=>OI.Id)
                .ToListAsync();
        }
    }

}
