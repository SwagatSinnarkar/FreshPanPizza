﻿using FreshPanPizza.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Areas.Admin.Controllers
{
    public class ItemController : BaseController
    {
        ICatalogService _catalogService;

        public ItemController(ICatalogService catalogService)
        {
                _catalogService = catalogService;
        }

        //Item List Show
        public IActionResult Index()
        {
            var data = _catalogService.GetItems();
            return View(data);
        }

        //Create Items
        public IActionResult Create()
        {           
            return View();
        }
    }
}
