using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shubhdecoration.Data.Account;

namespace ShubhDecoration.Controllers
{
    public class BaseController : Controller
    { 
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            var hasAllowAnonymous = filterContext.ActionDescriptor.EndpointMetadata
                .Any(em => em.GetType() == typeof(Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute));

            if (hasAllowAnonymous)
            {
                base.OnActionExecuting(filterContext);
                return;  
            }
             
            if (HttpContext.Session.GetString("UserId") == null)
            {
                filterContext.Result = new RedirectResult("/");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
        public SessionModel GetSession()
        {
            SessionModel _user = new SessionModel();
            try
            {
                var Token = HttpContext.Session.GetString("Token");
                var UserId = HttpContext.Session.GetString("UserId");
                var UserName = HttpContext.Session.GetString("UserName");
                var PackStationId = HttpContext.Session.GetString("PackStationId"); 
                _user.UserId = Convert.ToInt64(UserId);
                _user.UserName = UserName; 
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return _user;
        }
    }
}
