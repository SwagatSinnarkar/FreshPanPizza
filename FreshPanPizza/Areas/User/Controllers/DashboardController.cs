using FreshPanPizza.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Areas.User.Controllers
{
    public class DashboardController : BaseController
    {
        ICatalogService _catalogService;

        public DashboardController(ICatalogService catalogService)
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
