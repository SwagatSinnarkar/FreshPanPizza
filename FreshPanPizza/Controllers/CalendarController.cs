﻿using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CalendarFull()
        {
            return View();
        }
    }
}