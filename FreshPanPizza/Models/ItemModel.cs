using System.ComponentModel.DataAnnotations;

namespace FreshPanPizza.Models
{
    public class ItemModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public IFormFile? File { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name="Unit Price")]
        public decimal UnitPrice { get; set; }
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Item Type")]
        public int ItemTypeId { get; set; }
    }
}
