using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Business.Models
{
    public class Order:Entity
    {
        public List<OrderItem> Items { get; set; }
        public decimal TotalOrder { get; set; }
    }
}
