using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using ImageMagick;
using Lanting.IDCode.Application;
using Lanting.IDCode.Authorization;
using Lanting.IDCode.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lanting.IDCode.Sessions;
using Lanting.IDCode.Web.Models;

namespace Lanting.IDCode.Web.Mvc.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Codes)]
    public class ProductController : IDCodeControllerBase
    {
        private readonly IProductInfoAppService _appService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ISessionAppService _sessionAppService;
        private readonly string _filePath = "images/products/";
        public ProductController(IProductInfoAppService appService, IHostingEnvironment hostingEnvironment, ISessionAppService sessionAppService)
        {
            _appService = appService;
            _hostingEnvironment = hostingEnvironment;
            _sessionAppService = sessionAppService;
        }


        public async Task<IActionResult> Index()
        {
            var output = await _appService.GetAll(new PagedResultRequestDto { MaxResultCount = int.MaxValue }); //Paging not implemented yet
            return View(output);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var currentUser = await _sessionAppService.GetCurrentLoginInformations();
            ViewBag.UserName = currentUser.User.UserName;
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var currentUser = await _sessionAppService.GetCurrentLoginInformations();
            ViewBag.UserName = currentUser.User.UserName;
            var dto = await _appService.Get(new EntityDto(id));
            return View(dto);

        }


        [HttpPost]
        public async Task<ActionResult> FileUpload()
        {
            string filePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, _filePath);
            if (!System.IO.Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath);
            }
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
            {
                throw new Abp.UI.UserFriendlyException(417, "没有图片");
            }
            string fileType = file.FileName.Split(".").LastOrDefault();
            string[] fileTypeArr = new string[] { "jpg", "gif", "png", "jpeg" };
            if (string.IsNullOrEmpty(fileType) || !fileTypeArr.Contains(fileType.ToLower()))
            {
                throw new Abp.UI.UserFriendlyException(415, "非图片文件");
            }
            string fileName = DateTime.Now.ToString("yyyyMMddHHmm_") + file.FileName;
            string fullFilePath = System.IO.Path.Combine(filePath, fileName);
            using (System.IO.FileStream fs = System.IO.File.Create(fullFilePath))
            {
                await file.CopyToAsync(fs);
                await fs.FlushAsync();
            }

            var savedFile = new System.IO.FileInfo(fullFilePath);
            var optimizer = new ImageOptimizer();
            optimizer.Compress(savedFile);
            savedFile.Refresh();

            return Json(new
            {
                StatusCode = 200,
                Info = "图片上传成功！",
                Data = new
                {
                    url = string.Format("{0}{1}/{2}{3}", "Http://", Request.Host, _filePath, fileName),
                    name = fileName,
                }
            });
        }

        [HttpPost]
        [RequestFormLimits(
            BufferBody = false,
            BufferBodyLengthLimit = 0,
            KeyLengthLimit = 100000000,
            MemoryBufferThreshold = 0,
            MultipartBodyLengthLimit = 0,
            MultipartBoundaryLengthLimit = 0,
            MultipartHeadersCountLimit = 0,
            Order = 1,
            ValueCountLimit = 2,
            ValueLengthLimit = 100000000)]
        public async Task<JsonResult> SaveSnapshot([FromBody]ImageModel input)
        {
            var currentUser = await _sessionAppService.GetCurrentLoginInformations();
            string filePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, _filePath);
            bool saved = false;
            byte[] data = Convert.FromBase64String(input.DataUrl);

            string fileName = DateTime.Now.ToString("yyyyMMddHHmm") + ".png";
            string fullFilePath = System.IO.Path.Combine(filePath, fileName);

            System.IO.File.WriteAllBytes(fullFilePath, data);
            saved = true;
            return Json(saved ? "image saved" : "image not saved");
        }
    }
}