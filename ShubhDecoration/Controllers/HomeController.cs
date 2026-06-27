using CG.Web.MegaApiClient;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shubhdecoration.Data.Common;
using ShubhDecoration.Helper;
using ShubhDecoration.Models;
using System.Diagnostics;

namespace ShubhDecoration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            ResponseModel response = new ResponseModel();
            response = HttpContext.Session.GetObjectFromJson<ResponseModel>("response");
            return View(response);
        }
        //[AuthorizationAttribute]
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        { 
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        { 
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>(); 
            if (exceptionDetails != null)
            { 
                _logger.LogError($"Error Path: {exceptionDetails.Path} threw an exception: {exceptionDetails.Error.Message}");
            } 
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Decorator()
        {
            return View();
        } 
    }
}
