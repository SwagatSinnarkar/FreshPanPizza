using FreshPanPizza.Entities;
using FreshPanPizza.Repositories.Interfaces;
using FreshPanPizza.Services.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshPanPizza.Services.Implementations
{
    public class CatalogService : ICatalogService
    {
        //NOTE: In Catalog Service, we need Repository for category, itemType & item so that we can perform the Database
        //operation on item table, itemType table & Category Table.

        //IMP: Here, We define Generic Repository. So, we don`t have any specific repository for Item, Category & ItemType in FreshPanPizza.Repositories.
        //Because our plan is to perform only CRUD Operations.

        private readonly IRepository<Item> _itemRepo;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<ItemType> _itemTypeRepo;

        public CatalogService(IRepository<Item> itemRepo, IRepository<Category> categoryRepo, IRepository<ItemType> itemTypeRepo)
        {
            _itemRepo = itemRepo;
            _categoryRepo = categoryRepo;
            _itemTypeRepo = itemTypeRepo;
        }

        public void AddItem(Item item)
        {
            //Adding Item into the database.
            _itemRepo.Add(item);
            _itemRepo.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            _itemRepo.Delete(id);
            _itemRepo.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepo.GetAll();
        }

        public Item GetItem(int id)
        {
            return _itemRepo.Find(id);
        }

        public IEnumerable<Item> GetItems(int itemType, int categoryType)
        {
            IEnumerable<Item> items = _itemRepo.GetAll().OrderBy(item => item.CategoryId).ThenBy(item => item.ItemTypeId);


            //For Pizza & Non-Veg Pizza
            if (itemType == 1)
            {
                items = items.Where(x => x.ItemTypeId == itemType && x.ItemTypeId != 2 && x.CategoryId != 3 && x.CategoryId != 2);
            }
            else if (itemType == 2)
            {
                items = items.Where(x => x.ItemTypeId == itemType && x.ItemTypeId != 1 && x.CategoryId != 3 && x.CategoryId != 2);
            }
            else
            {
                //For Beverages & Sides
                if (categoryType == 2 || categoryType == 3)
                {
                    items = items.Where(x => x.CategoryId == categoryType);
                }
            }
            return items;
        }

        public IEnumerable<ItemType> GetItemType()
        {
            return _itemTypeRepo.GetAll();
        }

        public void UpdateItem(Item item)
        {
            _itemRepo.Update(item);
            _itemRepo.SaveChanges();
        }
    }
}
