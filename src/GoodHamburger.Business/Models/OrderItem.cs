using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Business.Models
{
    public class OrderItem:Entity
    {
       
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductType productType { get; set; }

        public int OrderId { get; set; }
        public Product Product { get; set; }
    }
}
