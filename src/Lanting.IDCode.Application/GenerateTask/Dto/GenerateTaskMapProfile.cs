
using AutoMapper;
using Lanting.IDCode.Entity;

namespace Lanting.IDCode.Application
{
    public class GenerateTaskMapProfile : Profile
    {
        public GenerateTaskMapProfile()
        {
            CreateMap<GenerateTaskDto, GenerateTask>();
            //CreateMap<GenerateTaskDto, GenerateTask>().ForMember(x => x.Subtables, opt => opt.Ignore());

            CreateMap<CreateGenerateTaskDto, GenerateTask>();
            //CreateMap<CreateGenerateTaskDto, GenerateTask>().ForMember(x => x.Product, opt => opt.Ignore());
        }
    }
}


