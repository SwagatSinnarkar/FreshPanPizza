using FreshPanPizza.Entities;
using FreshPanPizza.Repositories.Models;
using FreshPanPizza.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FreshPanPizza.Controllers
{
    public class CartController : BaseController
    {
        ICartService _cartService;

        //If you look at here, when you`re browsing any website so usually website allow you to create the shopping cart and adding the items to that.
        //So same statergy here we`re going to follow. we`ll allow user to create the shopping cart.
        //So ultimately we do create a GUID for shopping cart if the User is visiting to our website. Later on, the User will Login that time that Shopping Cart will map
        //to the our created User Acount detail.

        //Request.Cookies["CId"]: I`m using cookie for keeping my CartId, First time the user is visiting the page.
        Guid CartId
        {
            get
            {
                Guid Id;
                string CId = Request.Cookies["CId"]; //If Cart Id is already there in Cookie so I`m requesting from here. so use that CartId (CId) & returning to the property(CartId).
                if (string.IsNullOrEmpty(CId)) //If there is no cookie:
                {
                    Id = Guid.NewGuid(); //then creat a new GUID
                    Response.Cookies.Append("CId", Id.ToString()); //Write to the Cookie. So with Response.Cookies we`re creating the new Cookie with the name CId.
                } //So till the User is not Login to the website, we`ll use this CId without any User. As soon as, the User will login this "CId" will map with Login User.
                else
                {
                    Id = Guid.Parse(CId);
                }
                return Id;
            }

        }


        public CartController(ICartService cartService, UserManager<User> userManager) : base(userManager) 
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            //First, we need to create the Cart listing page where we`ll display the Cart info along with the CartItem.
            //So using with CartModel, we`ll get the Cart details here.
            CartModel cart = _cartService.GetCartDetails(CartId);            
            return View(cart);
        }

        

    }
}
