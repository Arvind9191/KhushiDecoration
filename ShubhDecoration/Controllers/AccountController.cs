using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Shubhdecoration.Data.Account;
using Shubhdecoration.Data.Common;
using Shubhdecoration.Repository.Dapper.Account;
using ShubhDecoration.Helper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;
namespace ShubhDecoration.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IAccountServices _accountService;
        private readonly IAccountRepo _accountrepo;
        public AccountController(IAccountRepo accountrepo)
        {
            //_accountService = accountService;
            _accountrepo = accountrepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountrepo.Login(model);
                    if (result != null && result.UserId > 0)
                    {
                        string role = string.Empty;
                        SessionModel sessionModel = new SessionModel();
                        sessionModel.UserId = result.UserId;
                        sessionModel.UserName = result.UserName;
                        var mobileNo = result.MobileNo;

                        var UserRole = result.UserRole;
                        var Email = result.EmailId;
                        if (((int)Roles.Admin) == result.UserRole)
                        {
                            sessionModel.Role = "Admin";
                        }
                        else if (((int)Roles.User) == result.UserRole)
                        {
                            sessionModel.Role = "User";
                        }
                        else if (((int)Roles.Employee) == result.UserRole)
                        {
                            sessionModel.Role = "Employee";
                        }
                        else if (((int)Roles.Manager) == result.UserRole)
                        {
                            sessionModel.Role = "Manager";
                        }
                        HttpContext.Session.SetString("UserName", sessionModel.UserName);
                        HttpContext.Session.SetString("UserId", sessionModel.UserId.ToString());
                        HttpContext.Session.SetString("UserRole", sessionModel.Role);
                        HttpContext.Session.SetString("MobileNo", mobileNo);
                        HttpContext.Session.SetString("Email", Email);
                        if (sessionModel != null)
                        {
                            if (UserRole == 1)
                            {
                                return RedirectToAction("Dashboard", "Account");
                            }
                            else if (UserRole == 2)
                            {
                                return RedirectToAction("Decorator", "Home");
                            }
                        }
                        else
                        {
                            TempData["Error"] = "This user and password not exists.";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "This user and password not exists.";
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error occurred while logging in user '{model.UserName}': {ex.Message}";

            }
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(SignupModel model)
        {
            try
            { 
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    var result = await _accountrepo.Registration(model);
                    if (result)
                    {
                        ResponseModel response = new ResponseModel
                        {
                            IsSuccess = true,
                            InfoType = 1,
                            Title = "Registration From Submit!",
                            Message = "Your Registration from successfully submited.Please  check your email and back to login "
                        };
                        HttpContext.Session.SetObjectAsJson("response", response);
                        return RedirectToAction("Index", "Home");
                    } 
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> Dashboard()
        {
            DashboardModel model = new DashboardModel();
            try
            {
                var result = await _accountrepo.UserList();
                if (result != null && result.Count > 0)
                {
                    model.UserList = result;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Enquiry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Enquiry(EnquiryViewModel model)
        {
            ModelState.Remove(""); 
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                EmailModel emailModel = new EmailModel();
                emailModel.ToEmail = model.Email;
                emailModel.CC = "arvindsarasawan@gmail.com";
                emailModel.Subject = $"New Event Enquiry from {model.FullName} - {DataHelper.CurrentDateTime()}";
                string htmlTemplate = $@"
                  <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #dcdcdc; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 8px rgba(0,0,0,0.05);'>
                <div style='background-color: #0099ca; padding: 20px; text-align: center; color: white;'>
                    <h2 style='margin: 0; font-size: 24px;'>This enquiry recieved from Khushi Decoration</h2>
                </div>
                <div style='padding: 25px; background-color: #ffffff; color: #333333;'>
                    <p style='font-size: 16px;'>Hello Admin,</p>
                    <p style='font-size: 15px; color: #555;'>A new customer has submitted an event enquiry.Here are the details:</p>
                    
                    <table style='width: 100%; border-collapse: collapse; margin-top: 20px;'>
                        <tr>
                            <td style='padding: 12px 10px; border-bottom: 1px solid #eeeeee; font-weight: bold; width: 35%; color: #0099ca;'>Full Name:</td>
                            <td style='padding: 12px 10px; border-bottom: 1px solid #eeeeee;'>{model.FullName}</td>
                        </tr>
                        <tr>
                            <td style='padding: 12px 10px; border-bottom: 1px solid #eeeeee; font-weight: bold; color: #0099ca;'>Email Address:</td>
                            <td style='padding: 12px 10px; border-bottom: 1px solid #eeeeee;'><a href='mailto:{model.Email}'>{model.Email}</a></td>
                        </tr>
                        <tr>
                            <td style='padding: 12px 10px; border-bottom: 1px solid #eeeeee; font-weight: bold; color: #0099ca;'>Phone Number:</td>
                            <td style='padding: 12px 10px; border-bottom: 1px solid #eeeeee;'><a href='tel:{model.Phone}'>{model.Phone}</a></td>
                        </tr>
                        <tr>
                            <td style='padding: 12px 10px; border-bottom: 1px solid #eeeeee; font-weight: bold; color: #0099ca;'>Event Type:</td>
                            <td style='padding: 12px 10px; border-bottom: 1px solid #eeeeee;'>{model.EventType}</td>
                        </tr>
                        <tr>
                            <td style='padding: 12px 10px; border-bottom: 1px solid #eeeeee; font-weight: bold; color: #0099ca;'>Event Date:</td>
                            <td style='padding: 12px 10px; border-bottom: 1px solid #eeeeee;'>{(model.EventDate.HasValue ? model.EventDate.Value.ToString("dd-MMM-yyyy") : "N/A")}</td>
                        </tr>
                        <tr>
                            <td style='padding: 12px 10px; border-bottom: 1px solid #eeeeee; font-weight: bold; color: #0099ca;'>Budget Range:</td>
                            <td style='padding: 12px 10px; border-bottom: 1px solid #eeeeee;'>{model.BudgetRange}</td>
                        </tr>
                        <tr>
                            <td style='padding: 12px 10px; font-weight: bold; vertical-align: top; color: #0099ca;'>Inquiry Details:</td>
                            <td style='padding: 12px 10px; background-color: #f8f9fa; border-radius: 4px; font-style: italic;'>{model.Message}</td>
                        </tr>
                    </table>
                </div>
                <div style='background-color: #f1f3f5; padding: 15px; text-align: center; font-size: 12px; color: #888;'>
                    <p style='margin: 0;'>This is an automated email generated securely from Shubhdecoration.</p>
                </div>
            </div>";
                emailModel.Body = htmlTemplate;
                emailModel.HtmlContent = htmlTemplate;
                bool isEmailSent = DataHelper.SendEmailDopmain(emailModel);
                if (isEmailSent)
                {
                    ResponseModel response = new ResponseModel
                    {
                        IsSuccess = true,
                        InfoType = 2,
                        Title = "ENQUIRY SEND!",
                        Message = "Your Enquiry from successfully send."
                    };
                    HttpContext.Session.SetObjectAsJson("response", response);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Sorry, we encountered an issue sending your enquiry. Please try again later.");
                    return View(model);
                } 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred while processing your request.");
                return View(model);
            }
        }
    }
}
