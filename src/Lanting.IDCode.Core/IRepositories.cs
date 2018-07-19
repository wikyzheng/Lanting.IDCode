using Abp.Domain.Repositories;
using Lanting.IDCode.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lanting.IDCode.Core.IRepositories
{
    public interface IIDentityCodeRepository : IRepository<IdentityCode, long>
    {
        Task<int> BatchInsert(IEnumerable<IdentityCode> list);
    }


}
