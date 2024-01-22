using FreshPanPizza.Entities;
using FreshPanPizza.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshPanPizza.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
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

        public IEnumerable<Order> GetUserOrders(int UserId)
        {
            return appContext.Orders.Include(x => x.OrderItems).Where(x => x.UserId == UserId).ToList();
        }
    }
}
