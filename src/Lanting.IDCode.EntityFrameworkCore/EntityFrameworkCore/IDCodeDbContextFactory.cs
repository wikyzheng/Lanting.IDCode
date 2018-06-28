using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Lanting.IDCode.Configuration;
using Lanting.IDCode.Web;

namespace Lanting.IDCode.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class IDCodeDbContextFactory : IDesignTimeDbContextFactory<IDCodeDbContext>
    {
        public IDCodeDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<IDCodeDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            IDCodeDbContextConfigurer.Configure(builder, configuration.GetConnectionString(IDCodeConsts.ConnectionStringName));

            return new IDCodeDbContext(builder.Options);
        }
    }
}
