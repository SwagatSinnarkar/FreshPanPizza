using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Controllers
{
    public class MapsController : Controller
    {
        // GET: Maps
        public IActionResult MapGoogle()
        {
            return View();
        }

        public IActionResult MapLeaflet()
        {
            return View();
        }

        public IActionResult MapVector()
        {
            return View();
        }

    }
}