using GoodHamburger.Business.Interfaces;
using GoodHamburger.Business.Models;
using GoodHamburger.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace GoodHamburger.Data.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(GHContext context) : base(context)
        {


        }

        public async Task<Order> SearchIdEnd()
        {
            return await _ghcontext.Orders.AsNoTracking()
                                            .OrderByDescending(O => O.Id)
                                            .FirstOrDefaultAsync();
                                                            
        }

        public async Task<Order> SearchIdOrder(int id)
        {
            return await _ghcontext.Orders.AsNoTracking()
                                            .Include(Oi => Oi.Items)
                                            .FirstOrDefaultAsync(O => O.Id == id);

        }
    }

}
