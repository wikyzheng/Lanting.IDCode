using Abp.EntityFrameworkCore;
using Lanting.IDCode.Core.IRepositories;
using Lanting.IDCode.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanting.IDCode.EntityFrameworkCore.Repositories
{
    public class IdentityCodeRepository : IDCodeRepositoryBase<IdentityCode, long>, IIDentityCodeRepository
    {
        public IdentityCodeRepository(IDbContextProvider<IDCodeDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<int> BatchInsert(IEnumerable<IdentityCode> list)
        {
            await Context.IdentityCodes.AddRangeAsync(list.ToArray());
            return await Context.SaveChangesAsync();
        }
    }
}
