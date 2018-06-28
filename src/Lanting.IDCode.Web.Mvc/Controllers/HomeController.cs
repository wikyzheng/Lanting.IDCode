using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Lanting.IDCode.Controllers;

namespace Lanting.IDCode.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : IDCodeControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
