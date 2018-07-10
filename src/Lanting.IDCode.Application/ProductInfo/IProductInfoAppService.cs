using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lanting.IDCode.Application
{
    public interface IProductInfoAppService : IAsyncCrudAppService<ProductInfoDto, int, PagedResultRequestDto, CreateProductInfoDto, ProductInfoDto>
    {
     
    }
}