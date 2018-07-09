using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Lanting.IDCode.Controllers;
using Lanting.IDCode.Sessions;
using System.Threading.Tasks;

namespace Lanting.IDCode.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : IDCodeControllerBase
    {
      
        public async Task<ActionResult> Index()
        {
           
            return View();
        }
    }
}
