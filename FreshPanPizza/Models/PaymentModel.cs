using FreshPanPizza.Repositories.Models;

namespace FreshPanPizza.Models
{
    //It`s containing information about payment gateway.
    public class PaymentModel
    {
        public string Name { get; set; } 
        public string RazorpayKey { get; set; } 
        public string GrandTotal { get; set; } 
        public string Description { get; set; } 
        public string Currency { get; set; } 
        public string OrderId { get; set; } 
        public CartModel Cart { get; set; } 
        public string Receipt { get; internal set; } 
    }
}
