using FreshPanPizza.Entities;
using FreshPanPizza.Helpers;
using FreshPanPizza.Models;
using FreshPanPizza.Repositories.Models;
using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            PaymentModel payment = new PaymentModel();
            CartModel cart = TempData.Peek<CartModel>("Cart");
            if (cart != null)
            {
                payment.Cart = cart;
            }

            //Payment label add to some calculation. Here, We calcualte the GrandTotal, currency we`re using because it`s require before doing the payment.
            payment.GrandTotal = Math.Round(cart.GrandTotal).ToString();
            payment.Currency = "INR";
            string items = "";
            foreach (var item in cart.Items)
            {
                items += item.Name + ",";
            }
            payment.Description = items;
            return View(payment);
        }
    }
}
