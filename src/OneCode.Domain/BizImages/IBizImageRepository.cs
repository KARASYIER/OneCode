using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OneCode.Domain.Repositories
{
    public interface IBizImageRepository : IRepository<BizImage, Guid>
    {
        Task<List<BizImage>> GetListAsync(Guid subjectId, BizImageScope scope);

    }
}
