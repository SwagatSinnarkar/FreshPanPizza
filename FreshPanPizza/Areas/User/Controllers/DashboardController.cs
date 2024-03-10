using FreshPanPizza.Interfaces;
using FreshPanPizza.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Areas.User.Controllers
{
    public class DashboardController : BaseController
    {
        ICatalogService _catalogService;

        public DashboardController(ICatalogService catalogService, IUserAccessor userAccessor) : base(userAccessor)
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
