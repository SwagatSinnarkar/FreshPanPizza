using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Controllers
{
    public class TablesController : Controller
    {
        // GET: Tables
        public IActionResult TableBasic()
        {
            return View();
        }

        public IActionResult TableDatatable()
        {
            return View();
        }

        public IActionResult TableEditable()
        {
            return View();
        }

        public IActionResult TableResponsive()
        {
            return View();
        }

    }
}