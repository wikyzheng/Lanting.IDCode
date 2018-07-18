using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Lanting.IDCode.Authorization.Roles;
using Lanting.IDCode.Authorization.Users;
using Lanting.IDCode.MultiTenancy;
using Lanting.IDCode.Entity;

namespace Lanting.IDCode.EntityFrameworkCore
{
    public class IDCodeDbContext : AbpZeroDbContext<Tenant, Role, User, IDCodeDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public IDCodeDbContext(DbContextOptions<IDCodeDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<ProductInfo> ProductInfos { get; set; }
        public virtual DbSet<GenerateTask> GenerateTasks { get; set; }
        public virtual DbSet<IdentityCode> IdentityCodes { get; set; }
        public virtual DbSet<TestKey> TtestKeys { get; set; }
    }
}
