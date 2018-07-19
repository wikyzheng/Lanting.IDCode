using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Lanting.IDCode.Application;
using Lanting.IDCode.Authorization;
using Lanting.IDCode.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace OneCode.BaseInfo.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Codes)]
    public class TaskController : IDCodeControllerBase
    {
        private readonly IGenerateTaskAppService _appService;
        private readonly IProductInfoAppService _productService;

        public TaskController(IGenerateTaskAppService appService, IProductInfoAppService productInfoAppService)
        {
            _appService = appService;
            _productService = productInfoAppService;

        }

        public async Task<IActionResult> Index()
        {
            var output = await _appService.GetAll(new PagedResultRequestDto { MaxResultCount = int.MaxValue }); //Paging not implemented yet
            var products = await _productService.GetAll(new PagedResultRequestDto { MaxResultCount = int.MaxValue });
            var selections = products.Items.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.FullName

            }).ToList();
            ViewBag.Products = selections;
            return View(output);
        }

        public async Task<ActionResult> EditGenerateTaskModal(int id)
        {
            var dto = await _appService.Get(new EntityDto(id));
            return View("_EditGenerateTaskModal", dto);
        }
    }

}