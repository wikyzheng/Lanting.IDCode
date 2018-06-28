using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Lanting.IDCode.Authorization.Users;
using Lanting.IDCode.Editions;

namespace Lanting.IDCode.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
