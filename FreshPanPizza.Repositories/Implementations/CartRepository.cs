using FreshPanPizza.Entities;
using FreshPanPizza.Repositories.Interfaces;
using FreshPanPizza.Repositories.Models;
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


        //Imp: For getting the Cart Details, We need to write the Join query as well because the Cart is not containing the 
        //info about the Item name, Item Unit Price.
        public CartModel GetCartDetails(Guid CartId)
        {
            //Fetching the Cart detail base upon the Cart Id & we`re checking wheather the Cart is Active or not.
            var model = (from cart in appContext.Carts
                         where cart.Id == CartId && cart.IsActive == true
                         select new CartModel  //We`re creating the object of the Cart model where we need the Items//*// list
                         {
                             Id = cart.Id,
                             UserId = cart.UserId,
                             CreatedDate = cart.CreatedDate,
                             //*//
                             Items = (from cartItem in appContext.CartItems
                                      join item in appContext.Items     //Why use join: Actually in Item table in database for shopping Cart, we never store the Item price
                                      on cartItem.ItemId equals item.Id  //because the Item price goes up & down. Even if store in the Cart level so we`ll not able to find out the
                                      where cartItem.CartId == CartId    //latest price whatever has been changed. That`s why we need to add here a Join query to the Item table.
                                      select new ItemModel              //So that I`ll get the price from the Item table whatever updated over there & CartItem only have the Cart Item details.    
                                      {
                                          Id = cartItem.Id,
                                          Name = item.Name,
                                          Description = item.Description,
                                          ImageUrl = item.ImageUrl,
                                          Quantity = cartItem.Quantity,
                                          ItemId = item.Id,
                                          UnitPrice = cartItem.UnitPrice
                                      }).ToList()
                         }).FirstOrDefault();
            return model;  //Returning Cart details along with item based upon the CartId.
        }
    }
}
