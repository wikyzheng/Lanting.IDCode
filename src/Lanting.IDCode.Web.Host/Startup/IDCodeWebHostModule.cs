using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Lanting.IDCode.Configuration;

namespace Lanting.IDCode.Web.Host.Startup
{
    [DependsOn(
       typeof(IDCodeWebCoreModule))]
    public class IDCodeWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public IDCodeWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(IDCodeWebHostModule).GetAssembly());
        }
    }
}
