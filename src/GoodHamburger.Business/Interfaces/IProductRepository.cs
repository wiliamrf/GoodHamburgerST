using GoodHamburger.Business.Models;

namespace GoodHamburger.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {

        public Task<Product> SearchId(int id);
        public decimal GetProductPriceByProductId(int productId);

        public ProductType GetProductProductTypeByProductId(int productId);
    }
}
