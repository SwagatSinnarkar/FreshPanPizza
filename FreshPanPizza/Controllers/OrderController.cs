using FreshPanPizza.Interfaces;
using FreshPanPizza.Models;
using FreshPanPizza.Repositories.Models;
using FreshPanPizza.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Controllers
{
    public class OrderController : BaseController
    {
        public readonly IOrderService _orderService;

        public OrderController(IOrderService orderService, IUserAccessor userAccessor) : base(userAccessor)        
        {
            _orderService = orderService;
        }
        
        public IActionResult Index()
        {
            var orders = _orderService.GetUserOrders(CurrentUser.Id);
            return View(orders);
        }

        [Route("~/User/Order/Details/{OrderId}")]
        public IActionResult Details(string OrderId)
        {
            OrderModel orderModel = _orderService.GetOrderDetails(OrderId);
            return View(orderModel);
        }
    }
}
