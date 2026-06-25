using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shubhdecoration.Data.Common;
using Shubhdecoration.Repository.Dapper.Common;

namespace ShubhDecoration.Controllers
{
    public class GeneralController : BaseController
    {
        private readonly ICommonRepository _commonrepo;
        List<Dropdown> ddlList = new List<Dropdown>();
        public GeneralController(ICommonRepository commonrepo)
        {
            _commonrepo = commonrepo;
        }
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]  
        [HttpGet]
        public IEnumerable<Dropdown> DdlCategory()
        {
            try
            {
                var response =   _commonrepo.DdlCotegoties();
                if (response != null && response.Count > 0)
                {
                    ddlList = response;
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return ddlList;
        }
    }
}
