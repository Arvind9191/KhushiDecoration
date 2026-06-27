using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizationAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var descriptor = context.ActionDescriptor as ControllerActionDescriptor; 
        var controllerName = descriptor?.ControllerName;
        var actionName = descriptor?.ActionName; 
        if (controllerName == "Account" &&  (actionName == "Login" || actionName == "Register"))
        {
            return;
        } 
        var session = context.HttpContext.Session;
        var userName = session.GetString("UserName");
        var role = session.GetString("UserRole"); 
        if (string.IsNullOrEmpty(userName))
        {
            context.Result = new RedirectToActionResult("Login","Account",null);
        }
    }
}