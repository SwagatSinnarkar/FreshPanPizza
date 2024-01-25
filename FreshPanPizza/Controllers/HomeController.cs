using FreshPanPizza.Entities;
using FreshPanPizza.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;

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
        public IActionResult VegPizza(int itemType, int categoryType)
        {
            var items = _catalogService.GetItems(itemType, categoryType); 
            return View(items);
        }
        //Non-Veg Pizza
        public IActionResult NonVegPizza(int itemType, int categoryType)
        {
            var items = _catalogService.GetItems(itemType, categoryType);
            return View(items);
        }
        //Beverages
        public IActionResult Beverages(int itemType, int categoryType)
        {
            var items = _catalogService.GetItems(itemType, categoryType);
            return View(items);
        }
        //Sides
        public IActionResult Sides(int itemType, int categoryType)
        {
            var items = _catalogService.GetItems(itemType, categoryType);
            return View(items);
        }
    }
}
