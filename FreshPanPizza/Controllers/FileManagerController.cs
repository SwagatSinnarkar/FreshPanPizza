using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Controllers
{
    public class FileManagerController : Controller
    {
        // GET: FileManager
        public IActionResult Index()
        {
            return View();
        }
    }
}