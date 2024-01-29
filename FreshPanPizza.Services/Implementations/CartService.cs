using FreshPanPizza.Entities;
using FreshPanPizza.Repositories.Interfaces;
using FreshPanPizza.Repositories.Models;
using FreshPanPizza.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshPanPizza.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IRepository<CartItem> _cartItem;

        public CartService(ICartRepository cartRepo, IRepository<CartItem> cartItem)
        {
            _cartRepo = cartRepo;
            _cartItem = cartItem;
        }
        public Cart AddItem(int UserId, Guid CartId, int ItemId, decimal UnitPrice, int Quantity)
        {
            try
            {
                Cart cart = _cartRepo.GetCart(CartId); //Using the Cart Repository, first we`re getting the Cart detail.

                if (cart == null)  //This is the way how the new Cart we`re creating and adding our first item to the shopping cart.
                {
                    cart = new Cart(); //If it`s a new Cart created the new Cart object.
                    CartItem item = new CartItem(ItemId, Quantity, UnitPrice);       
                    cart.Id = CartId;           //Add a new item.
                    cart.UserId = UserId;
                    cart.CreatedDate = DateTime.Now;

                    item.CartId = cart.Id;
                    cart.Items.Add(item);  //Added to the CartItem over there.
                    _cartRepo.Add(cart);
                    _cartRepo.SaveChanges();
                }
                else  //This is for the Existing Cart, here either we`re updating the Quantity if the item is already there or add the new item as a new one.
                {
                    CartItem item = cart.Items.Where(x => x.ItemId == ItemId).FirstOrDefault(); //If Cart is already created with this CartId then query the items from CartItem tables.
                    if (item != null) //Now just figure out, If the item is already there in the CartItem then just increase the Quantity over there. For ex: If one quantity is there just                                       
                    {                   // make 2,3 & so on.
                        item.Quantity += Quantity;
                        _cartItem.Update(item);
                        _cartItem.SaveChanges();
                    }
                    else
                    {
                        item = new CartItem(ItemId, Quantity, UnitPrice);  //If it`s a new Item then add a new Item over there by calling the Add method [ cart.Items.Add(item)]
                        item.CartId = cart.Id;
                        cart.Items.Add(item);

                        _cartItem.Update(item);
                        _cartItem.SaveChanges();
                    }
                }
                return cart;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int DeleteItem(Guid CartId, int ItemId)
        {
            return _cartRepo.DeleteItem(CartId, ItemId);
        }

        public int GetCartCount(Guid CartId)
        {
            var cart = _cartRepo.GetCart(CartId);
            return cart != null ? cart.Items.Count() : 0;
        }

        public CartModel GetCartDetails(Guid CartId)
        {
            var model = _cartRepo.GetCartDetails(CartId);
            if (model != null && model.Items.Count > 0)
            {
                decimal subTotal = 0;
                foreach (var item in model.Items)
                {
                    item.Total = item.UnitPrice * item.Quantity;
                    subTotal += item.Total;
                }
                //5% Tax
                model.Tax = Math.Round((model.Total * 5) / 100, 2);
                model.GrandTotal = model.Tax + model.Total;
            }
            return model;
        }

        //This is method is actually for Updating the UserId into the shopping Cart. Because here, we`ll allow the User to create the shopping Cart without Login.
        //So when the User will Login so that time we need to Update the actual UserId. So for updating the actual UserId this method "UpdateCart()" we`re using here.
        //So it`s not actually updating the shopping CartItem or anything. It`s only updating the User to the shopping Cart.
        public int UpdateCart(Guid CartId, int UserId) 
        {
            return _cartRepo.UpdateCart(CartId, UserId);
        }

        //So wher you will to + or - in quatity there you`re changing the quantity.
        public int UpdateQuantity(Guid CartId, int Id, int Quantity)
        {
            return _cartRepo.UpdateQuantity(CartId, Id, Quantity);
        }
    }
}
