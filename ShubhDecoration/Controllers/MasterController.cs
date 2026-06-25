using Microsoft.AspNetCore.Mvc;
using Shubhdecoration.Data.Decoration;
using Shubhdecoration.Repository.Master;
using ShubhDecoration.Helper;

namespace ShubhDecoration.Controllers
{
    public class MasterController : Controller
    {
        private readonly ILogger<MasterController> _logger;
        private readonly IMasterRepository _masterRepository;
        public MasterController(ILogger<MasterController> logger, IMasterRepository masterRepository)
        {
            _logger = logger;
            _masterRepository = masterRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateCategory()
        {
            Category model = new Category();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Kuch galat hua: " + ex.Message);
            }
            return View(model);
        }
    }
}
