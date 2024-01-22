using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public IActionResult Index()
        {
            return View();
        }
    }
}