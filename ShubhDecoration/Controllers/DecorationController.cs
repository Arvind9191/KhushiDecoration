using Microsoft.AspNetCore.Mvc;
using Shubhdecoration.Data.Decoration;
using Shubhdecoration.Repository.Dapper.Decoration;

namespace ShubhDecoration.Controllers
{
    public class DecorationController : Controller
    {
        private readonly IDecorationRepository _decoration;
        public DecorationController(IDecorationRepository decoration)
        {
            _decoration = decoration;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region----------Start Decoration ----------
        public async Task<IActionResult> AllDecoration()
        {
            AlldecorationModel model = new AlldecorationModel();
            try
            {
                model.decorations = await _decoration.GetAllDecoration(0, 0);
            }
            catch (Exception ex)
            {
            }
            return View(model);
        } 
        public async Task<IActionResult> MoreDetails(int cId, int dcId)
        {
            AlldecorationModel model = new AlldecorationModel();
            try
            {
                model.decorations = await _decoration.GetAllDecoration(cId, dcId);
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }  
        public async Task<IActionResult> DecorationDetails(int cId, int dcId)
        {
            AlldecorationModel model = new AlldecorationModel();
            try
            {
                model.decorations = await _decoration.GetAllDecoration(cId, dcId);
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        #endregion----------Start Decoration -------
    }
}
