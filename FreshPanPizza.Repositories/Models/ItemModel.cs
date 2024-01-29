using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshPanPizza.Repositories.Models
{
    //We`re keeping them separately, we`re not accessing directly the Item & Cart Entity.
    //Because it`s best practice to separate the dto from the entity level object.
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string ImageUrl { get; set; }
    }
}
