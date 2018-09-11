using Abp.Authorization;
using Abp.Domain.Repositories;
using Lanting.IDCode.Application;
using Lanting.IDCode.Controllers;
using Lanting.IDCode.Core.IRepositories;
using Lanting.IDCode.Entity;
using Lanting.IDCode.Sessions;
using Lanting.IDCode.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lanting.IDCode.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    public class HandelImageController : IDCodeControllerBase
    {
        private readonly IProductInfoAppService _appService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ISessionAppService _sessionAppService;
        private readonly IIDentityCodeRepository _identityCodeRepository;
        private readonly IRepository<ProductInfo> _productRepository;
        private readonly string _filePath = "images/products/";
        public HandelImageController(IProductInfoAppService appService, IHostingEnvironment hostingEnvironment, ISessionAppService sessionAppService, IIDentityCodeRepository identityCodeRepository, IRepository<ProductInfo> productRepository)
        {
            _appService = appService;
            _hostingEnvironment = hostingEnvironment;
            _sessionAppService = sessionAppService;
            _identityCodeRepository = identityCodeRepository;
            _productRepository = productRepository;
        }

        [AbpAllowAnonymous]
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
        public async Task<ActionResult> Post([FromBody]ImageModel input)
        {
            var currentUser = await _sessionAppService.GetCurrentLoginInformations();
            //get the current code and index and next code and product code
            string indexStr = input.FileName.Substring(6, 8);
            long index = long.Parse(indexStr);
            var codeRecord = await _identityCodeRepository.GetAsync(index);
            if (codeRecord == null)
                throw new Abp.UI.UserFriendlyException(404, "invalid code");
            if (!input.FileName.Equals(codeRecord.Code))
                throw new Abp.UI.UserFriendlyException(404, "invalid code");
            int folderIndex = (int)index / 1000;
            string folederName = $"{folderIndex * 1000 + 1}-{(folderIndex + 1) * 1000}";

            //if the current code is 1000's times under this proudct code, create new folder,and save the image to new folder

            var product = await _productRepository.GetAsync(codeRecord.ProductId);
            var productCode = product.Code;
            string _filePath = $"codepage\\{currentUser.User.UserName}\\label\\image\\{productCode}\\{folederName}";
            string fullFilePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, _filePath);
            if (!Directory.Exists(fullFilePath))
                Directory.CreateDirectory(fullFilePath);
            string fullImagePath = Path.Combine(fullFilePath, $"{indexStr}.png");
            byte[] data = Convert.FromBase64String(input.DataUrl);
            System.IO.File.WriteAllBytes(fullImagePath, data);

            var nextOne = await _identityCodeRepository.GetAsync(index + 1);
            string nextCode = string.Empty;
            if (nextOne != null)
                nextCode = nextOne.Code;

            return Json(new { next = nextCode });
        }
    }
}
