using FreshPanPizza.Helpers;
using FreshPanPizza.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Areas.User.Controllers
{
    [CustomAuthorize(Roles = "User")]
    [Area("User")]
    public class BaseController : Controller
    {
        public Entities.User CurrentUser
        {
            get
            {               
                //Ternary Operator
                return (User != null) ? _userAccessor.GetUser() : null;
            }
        }

        IUserAccessor _userAccessor;
        public BaseController(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }
    }
}
