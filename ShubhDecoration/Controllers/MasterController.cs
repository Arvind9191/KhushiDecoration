using Microsoft.AspNetCore.Mvc;
using Shubhdecoration.Data.Decoration;
using Shubhdecoration.Repository.Dapper.Master;
using ShubhDecoration.Helper;

namespace ShubhDecoration.Controllers
{
    [AuthorizationAttribute]
    public class MasterController : Controller
    {
        private readonly ILogger<MasterController> _logger;
        private readonly IMasterRepository _masterRepository;
        private readonly IConfiguration _configuration;
        public MasterController(ILogger<MasterController> logger, IMasterRepository masterRepository, IConfiguration configuration)
        {
            _logger = logger;
            _masterRepository = masterRepository;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CategoryList()
        {
            AlldecorationModel model = new AlldecorationModel();
            model.CategoryList = await _masterRepository.CategoryListOrSingle(0);
            return View(model);
        }
        public IActionResult CreateCategory()
        {
            Category model = new Category();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category model)
        {
            ModelState.Remove("CreatedAt");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                model.CreatedAt = DataHelper.CurrentDate("dd/MM/yyyy HH:mm:ss");
                var res = await _masterRepository.InsertCategoryAsync(model);
                if (res)
                {
                    TempData["SuccessMessage"] = "Category successfully create ho gayi hai!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Database me data save nahi ho paya.");
                }
            }
            return View(model);
        }
    }
}
