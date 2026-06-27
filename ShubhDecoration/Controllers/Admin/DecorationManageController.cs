using Microsoft.AspNetCore.Mvc;
using Shubhdecoration.Data.Decoration;
using Shubhdecoration.Repository.Dapper.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shubhdecoration.Data.Account;
using Shubhdecoration.Repository.Dapper.Decoration;

namespace ShubhDecoration.Controllers.Admin
{ 
    [AuthorizationAttribute] 
    public class DecorationManageController : Controller
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IDecorationRepository _decorationrepo;
        public DecorationManageController(ICommonRepository commonRepository, IDecorationRepository decorationrepo)
        {
            _commonRepository = commonRepository;
            _decorationrepo = decorationrepo;
        }
        [HttpGet]
        public IActionResult CreateCard()
        {
            ViewBag.DdlCategory = new SelectList(_commonRepository.DdlCotegoties(), "Value", "Text");
            return View(new CreateCardModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCard(CreateCardModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _decorationrepo.CreateDecoCard(model);
                    if (result != null)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.DdlCategory = new SelectList(_commonRepository.DdlCotegoties(), "Value", "Text");
            return View(model);
        }
    }
}