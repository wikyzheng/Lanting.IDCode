using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Lanting.IDCode.Authorization;
using Lanting.IDCode.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lanting.IDCode.Web.Mvc.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Codes)]
    public class ProductController : IDCodeControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}