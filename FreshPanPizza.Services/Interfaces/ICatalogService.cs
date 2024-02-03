using FreshPanPizza.Entities;

namespace FreshPanPizza.Services.Interfaces
{
    public interface ICatalogService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<ItemType> GetItemType();
        IEnumerable<Item> GetItems(int itemType, int categoryType);
        IEnumerable<Item> GetItems1();
        Item GetItem(int id);
        void AddItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(int id);
    }
}
