using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Lanting.IDCode.Authorization.Roles;
using Lanting.IDCode.Authorization.Users;
using Lanting.IDCode.MultiTenancy;

namespace Lanting.IDCode.EntityFrameworkCore
{
    public class IDCodeDbContext : AbpZeroDbContext<Tenant, Role, User, IDCodeDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public IDCodeDbContext(DbContextOptions<IDCodeDbContext> options)
            : base(options)
        {
        }
    }
}
