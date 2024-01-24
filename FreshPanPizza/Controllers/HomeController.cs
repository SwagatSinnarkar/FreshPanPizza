using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
