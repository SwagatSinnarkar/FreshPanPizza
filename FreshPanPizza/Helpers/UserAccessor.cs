using FreshPanPizza.Entities;
using FreshPanPizza.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FreshPanPizza.Helpers
{
    public class UserAccessor : IUserAccessor
    {
        //We use UserManager & IHttpContextAccessor so that we can access the Login user detail.
        //IHttpContextAccessor will provide us the information about the Http request context. So from context label we can access Login user detail.
        private readonly UserManager<User> _userManager;
        private IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public User GetUser()
        {
            //HttpContext.User -> This only containing the Login user name it`s not containing the complete user object.
            if (_httpContextAccessor.HttpContext.User!=null)
            {
                //So, that`s why using HttpContext.User with the help of "_userManager" user manager class with GetUserAsync I`m fetching the complete user detail.
                return _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
            }
            else
            {
                return null;
            }
        }
    }
}
