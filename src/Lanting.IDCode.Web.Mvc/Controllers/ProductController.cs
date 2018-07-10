using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Lanting.IDCode.Application;
using Lanting.IDCode.Authorization;
using Lanting.IDCode.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lanting.IDCode.Web.Mvc.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Codes)]
    public class ProductController : IDCodeControllerBase
    {
        private readonly IProductInfoAppService _appService;

        public ProductController(IProductInfoAppService appService)
        {
            _appService = appService;

        }

        public async Task<IActionResult> Index()
        {
            var output = await _appService.GetAll(new PagedResultRequestDto { MaxResultCount = int.MaxValue }); //Paging not implemented yet
            return View(output);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> EditProductInfoModal(int id)
        {
            var dto = await _appService.Get(new EntityDto(id));
            return View("_EditProductInfoModal", dto);
        }
    }
}