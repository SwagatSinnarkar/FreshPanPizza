using FreshPanPizza.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    [Area("Admin")]
    public class BaseController : Controller
    {

    }
}
