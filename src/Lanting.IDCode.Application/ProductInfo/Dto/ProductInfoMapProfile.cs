
using AutoMapper;
using Lanting.IDCode.Entity;

namespace Lanting.IDCode.Application
{
    public class ProductInfoMapProfile : Profile
    {
        public ProductInfoMapProfile()
        {
            CreateMap<ProductInfoDto, ProductInfo>();
            //CreateMap<ProductInfoDto, ProductInfo>().ForMember(x => x.Subtables, opt => opt.Ignore());

            CreateMap<CreateProductInfoDto, ProductInfo>();
            //CreateMap<CreateProductInfoDto, ProductInfo>().ForMember(x => x.Subtables, opt => opt.Ignore());
        }
    }
}


