using FreshPanPizza.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Controllers
{
    public class HomeController : Controller
    {
        ICatalogService _catalogService;

        public HomeController(ICatalogService catalogService)
        {
                _catalogService = catalogService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Veg Pizza
        public IActionResult VegPizza(int itemType)
        {
            var items = _catalogService.GetItems(itemType); 
            return View(items);
        }
        //Non-Veg Pizza
        public IActionResult NonVegPizza()
        {
            return View();
        }
        //Beverages
        public IActionResult Beverages()
        {
            return View();
        }
        //Sides
        public IActionResult Sides()
        {
            return View();
        }
    }
}
