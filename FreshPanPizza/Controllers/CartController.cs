using FreshPanPizza.Entities;
using FreshPanPizza.Helpers;
using FreshPanPizza.Interfaces;
using FreshPanPizza.Repositories.Models;
using FreshPanPizza.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
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


        public CartController(ICartService cartService, IUserAccessor userAccessor) : base(userAccessor)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            //First, we need to create the Cart listing page where we`ll display the Cart info along with the CartItem.
            //So using with CartModel, we`ll get the Cart details here.

            //If user is logged in, so, we need to get the logged in user shopping cart. 

            //The LoggedIn Cart detail
            CartModel cart = _cartService.GetCartDetails(CartId);
            if (CurrentUser != null && cart != null)
            {
                TempData.Set("Cart", cart);

                //So we should actually update the Cart along with the loggedIn user userId. It`ll help us when user is loggedIn & directly he is doing the Payment.
                _cartService.UpdateCart(cart.Id, CurrentUser.Id); 
            }
            return View(cart);
        }

        [Route("Cart/AddToCart/{ItemId}/{UnitPrice}/{Quantity}")]
        public IActionResult AddToCart(int ItemId, decimal UnitPrice, int Quantity)
        {
            int UserId = CurrentUser != null ? CurrentUser.Id : 0;
            if (ItemId > 0 && Quantity > 0)
            {
                Cart cart = _cartService.AddItem(UserId, CartId, ItemId, UnitPrice, Quantity);
                var data = JsonSerializer.Serialize(cart);
                return Json(data);
            }
            else
            {
                return Json("");
            }
        }

        public IActionResult DeleteItem(int Id)
        {
            int count = _cartService.DeleteItem(CartId, Id);
            return Json(count);
        }

        [Route("Cart/UpdateQuantity/{Id}/{Quantity}")]
        public IActionResult UpdateQuantity(int Id, int Quantity)
        {
            int count = _cartService.UpdateQuantity(CartId, Id, Quantity);
            return Json(count);
        }

        public IActionResult GetCartCount()
        {
            int count = _cartService.GetCartCount(CartId);
            return Json(count);
        }

        //CheckOut
        public IActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckOut(Address address)
        {
            //We can set the Address here in TempData. So, for TempData we can create an Extension class so that we can simplify the serialization & De-serialization
            //Because C# list & the C# object directly if you try to assign to TempData it`ll create the Serialization issue. So by creating the TempData Extension which
            //be useful for setting the object or list & it should be static class because Extension method are the part of the static class.
            TempData.Set("Address", address);       
            return RedirectToAction("Index", "Payment");
        }
    }
}

