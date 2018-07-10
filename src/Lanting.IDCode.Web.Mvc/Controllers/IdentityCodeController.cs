using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Lanting.IDCode.Application;
using Lanting.IDCode.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OneCode.BaseInfo.Web.Controllers
{

    public class IdentityCodeController : IDCodeControllerBase
    {
        private readonly IIdentityCodeAppService _appService;

        public IdentityCodeController(IIdentityCodeAppService appService)
        {
            _appService = appService;

        }

        public async Task<IActionResult> Index()
        {
            var output = await _appService.GetAll(new PagedResultRequestDto { MaxResultCount = int.MaxValue }); //Paging not implemented yet
            return View(output);
        }

        public async Task<ActionResult> EditIdentityCodeModal(long id)
        {
            var dto = await _appService.Get(new EntityDto<long>(id));
            return View("_EditIdentityCodeModal", dto);
        }
    }

}