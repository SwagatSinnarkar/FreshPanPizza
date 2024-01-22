using FreshPanPizza.Entities;
using FreshPanPizza.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FreshPanPizza.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        protected SignInManager<User> _signInManager;
        protected UserManager<User> _userManager;
        protected RoleManager<Role> _roleManager;

        public AuthenticationService(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public User AuthenticateUser(string Username, string Password)
        {
            var result = _signInManager.PasswordSignInAsync(Username, Password, false, lockoutOnFailure: false).Result;
            if (result.Succeeded)
            {
                var user = _userManager.FindByNameAsync(Username).Result;
                var roles = _userManager.GetRolesAsync(user).Result;
                user.Roles = roles.ToArray();

                return user;
            }
            return null;
        }

        public bool CreateUser(User user, string Password)
        {
            var result = _userManager.CreateAsync(user, Password).Result;
            if (result.Succeeded)
            {
                //Admin, User
                //string role = "User";                      //role
                var res = _userManager.AddToRoleAsync(user, user.Role).Result;
                if (res.Succeeded)
                {
                    return true;
                }

            }
            return false;
        }

        public User GetUser(string Username)
        {
            return _userManager.FindByNameAsync(Username).Result;
        }

        public async Task<bool> SignOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
