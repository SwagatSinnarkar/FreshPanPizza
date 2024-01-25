using FreshPanPizza.Entities;
using FreshPanPizza.Models;
using FreshPanPizza.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshPanPizza.Controllers
{
    public class AccountController : Controller
    {

        IAuthenticationService _authService;

        //account controller const
        public AccountController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        //For SignUp process we need to define the UserModel so that we can define the Validation logic over there.
        //Like Username, Password is required.
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UserModel model)
        {
            if (ModelState.IsValid)
            {
                string role = "User";
                User user = new User
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Role = role
                };

                bool result = _authService.CreateUser(user, model.Password);
                if (result)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _authService.AuthenticateUser(model.Email, model.Password);
                if (user != null)
                {
                    if (user.Roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                    }
                    else if (user.Roles.Contains("User"))
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "User" });                   
                    }
                }
            }
            return View();
        }

        //Defining the separate ActionResult for LogOutComplete, the reason is if we return View(), then what will happen the loggedin
        //user detail will not be clear completely from the user browser, it`s using cookie so that`s why I`m going to use to redirect
        //so that in Redirection the cookie will Refresh. so, this reason to create the separate view & we`re redirecting to the new action.
        public IActionResult LogOutComplete()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            //It will take some time to clear the cookie, login user detail.
            await _authService.SignOut();
            return RedirectToAction("LogOutComplete");
        }

        public IActionResult UnAuthorize()
        {
            return View();
        }

    }
}
