using FreshPanPizza.Entities;
using FreshPanPizza.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;

namespace FreshPanPizza.ViewComponents
{
    public class PizzaMenuViewComponent : ViewComponent
    {
        ICatalogService _catalogService;

        public PizzaMenuViewComponent(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/ViewComponentLayouts/_PizzaMenuIndex.cshtml");
        }
    }
}
