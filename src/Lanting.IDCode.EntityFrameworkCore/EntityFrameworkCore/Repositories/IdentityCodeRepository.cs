using Abp.EntityFrameworkCore;
using Lanting.IDCode.Core.IRepositories;
using Lanting.IDCode.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lanting.IDCode.EntityFrameworkCore.Repositories
{
    public class IdentityCodeRepository : IDCodeRepositoryBase<IdentityCode, long>, IIDentityCodeRepository
    {
        public IdentityCodeRepository(IDbContextProvider<IDCodeDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task BatchInsert(IEnumerable<IdentityCode> list)
        {
            await this.GetDbContext().BulkInsertAsync(list);
        }
    }
}
