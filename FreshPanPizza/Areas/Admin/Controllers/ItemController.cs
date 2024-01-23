using FreshPanPizza.Entities;
using FreshPanPizza.Interfaces;
using FreshPanPizza.Models;
using FreshPanPizza.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Areas.Admin.Controllers
{
    public class ItemController : BaseController
    {
        ICatalogService _catalogService;
        IFileHelper _fileHelper;

        public ItemController(ICatalogService catalogService, IFileHelper fileHelper)
        {
            _catalogService = catalogService;
            _fileHelper = fileHelper;
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
            ViewBag.Categories = _catalogService.GetCategories();
            ViewBag.ItemTypes = _catalogService.GetItemType();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ItemModel itemModel)
        {
            try
            {
                itemModel.ImageUrl = _fileHelper.UploadFile(itemModel.File);
                Item data = new Item
                {
                    Name = itemModel.Name,
                    UnitPrice = itemModel.UnitPrice,
                    CategoryId = itemModel.CategoryId,
                    ItemTypeId = itemModel.ItemTypeId,
                    Description = itemModel.Description,
                    ImageUrl = itemModel.ImageUrl
                };

                _catalogService.AddItem(data);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

            }

            //Rebinding.
            ViewBag.Categories = _catalogService.GetCategories();
            ViewBag.ItemTypes = _catalogService.GetItemType();
            return View();
        }
    }
}
