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

        public IActionResult SingleDashboard(int itemType, int categoryType)
        {
            var items = _catalogService.GetItems(itemType, categoryType);
            return View(items);

        }

    }
}
