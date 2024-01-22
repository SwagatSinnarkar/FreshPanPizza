using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Controllers
{
    public class TasksController : Controller
    {
        // GET: Tasks
        public IActionResult TaskCreate()
        {
            return View();
        }

        public IActionResult TaskKanban()
        {
            return View();
        }

        public IActionResult TaskList()
        {
            return View();
        }

    }
}