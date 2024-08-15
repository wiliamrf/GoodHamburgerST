using GoodHamburger.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Business.Interfaces
{
    public interface IOrderItemRepository:IRepository<OrderItem>
    {

        public Task<OrderItem> SearchIdOrderItem(int id);

        public Task<IEnumerable<OrderItem>> SearchIdOrderItemlist(int id);
    }
}
