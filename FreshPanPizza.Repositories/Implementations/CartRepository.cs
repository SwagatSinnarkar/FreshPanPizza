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
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private AppDBContext appContext
        {
            get
            {
                return _dbContext as AppDBContext; //so from DbContext we`ve retrieve the app context.
            }
        }

        public CartRepository(DbContext dbContext) : base(dbContext)
        {

        }
        public Cart GetCart(Guid CartId)
        {
            //We`ll get the Cart along with the Cart Items. We aren`t using Find() method here because it`ll return only Cart data not a CartItems.
            return appContext.Carts.Include("Items").Where(x => x.Id == CartId && x.IsActive == true).FirstOrDefault();
        }

        public int DeleteItem(Guid cartId, int itemId)
        {
            var item = appContext.CartItems.Where(x => x.CartId == cartId && x.Id == itemId).FirstOrDefault();
            if (item != null)
            {
                appContext.CartItems.Remove(item);
                return appContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public int UpdateQuantity(Guid cartId, int itemId, int Quantity)
        {
            bool flag = false;
            var cart = GetCart(cartId);
            if (cart != null)
            {
                for (int i = 0; i < cart.Items.Count; i++)
                {
                    if (cart.Items[i].Id == itemId)
                    {
                        flag = true;
                        //for minus quantity
                        if (Quantity < 0 && cart.Items[i].Quantity > 1)
                        {
                            cart.Items[i].Quantity += (Quantity);
                        }
                        else if (Quantity > 0)
                        {
                            cart.Items[i].Quantity += (Quantity);
                        }
                        break;
                    }
                }
                if (flag) { return appContext.SaveChanges(); }
            }
            return 0;
        }

        public int UpdateCart(Guid cartId, int userId)
        {
            Cart cart = GetCart(cartId);
            cart.UserId = userId;   
            return appContext.SaveChanges(); 
        }
    }
}
