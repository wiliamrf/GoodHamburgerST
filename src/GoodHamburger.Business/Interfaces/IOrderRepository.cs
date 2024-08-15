using GoodHamburger.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Business.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {

        public Task<Order> SearchIdOrder(int id);
        public Task<Order> SearchIdEnd();

    }
}
