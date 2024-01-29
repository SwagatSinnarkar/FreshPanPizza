using FreshPanPizza.Entities;
using FreshPanPizza.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshPanPizza.Services.Interfaces
{
    public interface ICartService
    {
        //Comman Items in the Cart counts.
        int GetCartCount(Guid CartId);

        //Cart Details
        CartModel GetCartDetails(Guid CartId);

        //Add items to shopping Cart
        Cart AddItem(int UserId, Guid CartId, int ItemId, decimal UnitPrice, int Quantity);

        //Delete items from shopping Cart
        int DeleteItem(Guid CartId, int ItemId);

        //Update the Quantity
        int UpdateQuantity(Guid CartId, int Id, int Quantity);
        
        //Update the shopping Cart base on UserId.
        int UpdateCart(Guid CartId, int UserId);
    }
}
