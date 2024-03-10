using FreshPanPizza.Repositories.Interfaces;
using FreshPanPizza.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace FreshPanPizza.Repositories.Implementations
{
    public class OrderRepository : Repository<Entities.Order>, IOrderRepository
    {
        private AppDBContext appContext
        {
            get
            {
                return _dbContext as AppDBContext; //so from DbContext we`ve retrieve the app context.
            }
        }

        public OrderRepository(DbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Entities.Order> GetUserOrders(int UserId)
        {
            return appContext.Orders.Include(x => x.OrderItems).Where(x => x.UserId == UserId).ToList();
        }

        public OrderModel GetOrderDetails(string orderId)
        {
            var model = (from order in appContext.Orders
                         where order.Id == orderId
                         select new OrderModel
                         {
                             Id = order.Id,
                             UserId = order.UserId,
                             CreatedDate = order.CreatedDate,
                             Items = (from orderItem in appContext.OrderItems
                                      join item in appContext.Items
                                      on orderItem.ItemId equals item.Id
                                      where orderItem.OrderId == orderId
                                      select new ItemModel
                                      {
                                          Id = orderItem.Id,
                                          Name = item.Name,
                                          Description = item.Description,
                                          ImageUrl = item.ImageUrl,
                                          Quantity = orderItem.Quantity,
                                          ItemId = item.Id,
                                          UnitPrice = orderItem.UnitPrice
                                      }).ToList()
                         }).FirstOrDefault();
            return model;
        }     
    }
}
