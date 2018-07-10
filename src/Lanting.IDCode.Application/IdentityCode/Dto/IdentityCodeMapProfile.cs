
using AutoMapper;
using Lanting.IDCode.Entity;

namespace Lanting.IDCode.Application
{
    public class IdentityCodeMapProfile : Profile
    {
        public IdentityCodeMapProfile()
        {
            CreateMap<IdentityCodeDto, IdentityCode>();
            //CreateMap<IdentityCodeDto, IdentityCode>().ForMember(x => x.Subtables, opt => opt.Ignore());

            CreateMap<CreateIdentityCodeDto, IdentityCode>();
            //CreateMap<CreateIdentityCodeDto, IdentityCode>().ForMember(x => x.Subtables, opt => opt.Ignore());
        }
    }
}


