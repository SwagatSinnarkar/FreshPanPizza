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

        public IActionResult SingleDashboard(int itemType, int categoryType)
        {
            var items = _catalogService.GetItems(itemType, categoryType);
            return View(items);

        }
    }
}
