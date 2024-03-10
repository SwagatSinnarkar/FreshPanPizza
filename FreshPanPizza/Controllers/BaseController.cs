using FreshPanPizza.Entities;
using FreshPanPizza.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Controllers
{
    public class BaseController : Controller
    {
        //protected UserManager<User> _userManager;
        //public User CurrentUser
        //{
        //    get {
        //        if (User.Identity.Name != null)
        //        {
        //            return _userManager.FindByNameAsync(User.Identity.Name).Result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public BaseController(UserManager<User> userManager)
        //{
        //        _userManager = userManager;
        //}


        public User CurrentUser
        {
            get
            {
                //if (User != null)
                //    return _userAccessor.GetUser();
                //else
                //    return null;

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
