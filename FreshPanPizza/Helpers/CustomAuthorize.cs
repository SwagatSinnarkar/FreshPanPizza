using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FreshPanPizza.Helpers
{
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //User Authentication
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                //User Authorization: Feth the user permission from database 
                if (!context.HttpContext.User.IsInRole(Roles))
                {
                    //So if the login user containing these "Roles" which will be set up , we`ll apply this "CustomAuthorize" attribute if it`s containing it means if user have
                    //the permission to access the particular action, controller. If user don`t have the role than user do not have the permission.
                    //Roles - Admin or User
                    //If user do not have the role we`ll redirect the user to Unauthorize page.
                    context.Result = new RedirectToActionResult("UnAuthorize", "Account", new { area = "" });
                }                
            }
            else
            {
                //if user is not authenticated so let`s redirect user to login page.
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
            }
        }
    }
}
