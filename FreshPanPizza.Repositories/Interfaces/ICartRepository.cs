using FreshPanPizza.Entities;
using FreshPanPizza.Repositories.Implementations;

namespace FreshPanPizza.Repositories.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetCart(Guid CartId);
        int DeleteItem (Guid cartId, int itemId);   
        int UpdateQuantity (Guid cartId, int itemId, int Quantity);   
        int UpdateCart (Guid cartId, int userId);   
    }
}
