using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Controllers
{
    public class CandidateController : Controller
    {
        // GET: Candidate
        public IActionResult list()
        {
            return View();
        }
        public IActionResult Overview()
        {
            return View();
        }
    }
}