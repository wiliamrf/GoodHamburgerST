using GoodHamburger.Business.Models;

namespace GoodHamburger.Business.Interfaces
{
    public interface IOrderService :IDisposable
    {
        Task<bool> Add(Order order);
        Task<bool> Update(Order order);
        Task<bool> Delete(int id);
        
    }
}
