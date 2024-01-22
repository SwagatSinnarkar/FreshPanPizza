using FreshPanPizza.Helpers;
using FreshPanPizza.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Areas.User.Controllers
{
    [CustomAuthorize(Roles = "User")]
    [Area("User")]
    public class BaseController : Controller
    {
       
    }
}
