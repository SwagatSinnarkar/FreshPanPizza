using FreshPanPizza.Entities;
using FreshPanPizza.Interfaces;
using FreshPanPizza.Models;
using FreshPanPizza.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var data = _catalogService.GetItems(0);
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

        //Edit
        //Based on id I`ll get item details from the Catalog service.
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _catalogService.GetCategories();
            ViewBag.ItemTypes = _catalogService.GetItemType();
            Item data = _catalogService.GetItem(id);
            ItemModel itemModel = new ItemModel
            {
                Id = data.Id,
                Name = data.Name,
                UnitPrice = data.UnitPrice,
                CategoryId = data.CategoryId,
                ItemTypeId = data.ItemTypeId,
                Description = data.Description,
                ImageUrl = data.ImageUrl               
            };

            //we`re returning the model in the same Create view, I`m not creating separate entity view because similar view already there.
            return View("Create", itemModel);
        }

        [HttpPost]
        public IActionResult Edit(ItemModel model)
        {
            try
            {                               
                if (model.File != null)
                {
                    //When clicking on save button, first we`re deleting the existing file if it`s there.
                    _fileHelper.DeleteFile(model.ImageUrl);
                    //If there is no file over there, then we`ll upload the new picture.
                    model.ImageUrl = _fileHelper.UploadFile(model.File);
                }

                //Then we`ll send the model value to the item entity.
                Item data = new Item
                {
                    Id = model.Id,
                    Name = model.Name,
                    UnitPrice = model.UnitPrice,
                    CategoryId = model.CategoryId,
                    ItemTypeId = model.ItemTypeId,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl
                };

                _catalogService.UpdateItem(data);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

            }
            ViewBag.Categories = _catalogService.GetCategories();
            ViewBag.ItemTypes = _catalogService.GetItemType();
            return View("Create", model);
        }

        //Delete
        //string url: So, I`m keeping the image url separately because I don`t want to hit the database again to fetch the item details
        [Route("~/Admin/Item/Delete/{id}/{url}")]
        public IActionResult Delete(int id, string url)
        {
            //When we`re taking a file Url so as well as it`s going to replace by % & 2 of character, so we`re using below string replace.
            url = url.Replace("%2F", "/"); //replace to find the file.
            _catalogService.DeleteItem(id);
            _fileHelper.DeleteFile(url);
            return RedirectToAction("Index");
        }
        
    }
}
