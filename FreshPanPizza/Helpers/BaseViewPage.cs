using FreshPanPizza.Entities;
using FreshPanPizza.Interfaces;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace FreshPanPizza.Helpers
{
    public abstract class BaseViewPage<TModel> : RazorPage<TModel>
    {

        //RazorInject is an attribute which can use with this RazorPage.
        [RazorInject]
        public IUserAccessor _userAccessor { get; set; }

        public User CurentUser
        {
            //Here, the LoggedIn details, we need to fetch with the help of the user manager.
            //So better way here instead of writing the code of user manager over there let`s define here the user accessor.
            //Separate class defining for user accessor using with we`ll access the loogedin user detail with the help of user manager class.
            get
            {
                if (User!=null)
                {
                    return _userAccessor.GetUser();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
