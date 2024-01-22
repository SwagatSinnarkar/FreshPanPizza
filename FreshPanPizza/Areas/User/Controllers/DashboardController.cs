using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Areas.User.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
