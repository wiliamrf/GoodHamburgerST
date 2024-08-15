using GoodHamburger.Business.Interfaces;
using GoodHamburger.Business.Models;
using GoodHamburger.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GoodHamburger.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(GHContext context) : base(context)
        {
        }



        public async Task<Product> SearchId(int id)
        {

            var product = await _ghcontext.Products.AsNoTracking()
           .FirstOrDefaultAsync(P => P.Id == id);
            return product;
        }

        public decimal GetProductPriceByProductId(int productId)
        {
            var product = _ghcontext.Products.AsNoTracking().FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                return product.Price;
            }
            return 0;

        }
        public ProductType GetProductProductTypeByProductId(int productId)
        {
            var product = _ghcontext.Products.AsNoTracking().FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                return product.productType;
            }
            return 0;


        }
    }
}
