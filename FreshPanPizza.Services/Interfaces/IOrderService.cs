using FreshPanPizza.Entities;
using FreshPanPizza.Repositories.Models;

namespace FreshPanPizza.Services.Interfaces
{
    public interface IOrderService
    {
        OrderModel GetOrderDetails(string OrderId);
        IEnumerable<Entities.Order> GetUserOrders(int UserId);
        int PlaceOrder(int userId, string orderId, string paymentId, CartModel cart, Address address);
        
    }
}
