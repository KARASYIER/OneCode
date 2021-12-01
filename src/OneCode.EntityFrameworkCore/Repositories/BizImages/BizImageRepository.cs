using Microsoft.EntityFrameworkCore;
using OneCode.Domain;
using OneCode.Domain.Repositories;
using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OneCode.EntityFrameworkCore.Repositories
{
    public class BizImageRepository : EfCoreRepository<OneCodeDbContext, BizImage, Guid>, IBizImageRepository
    {
        public BizImageRepository(IDbContextProvider<OneCodeDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<List<BizImage>> GetListAsync(Guid subjectId, BizImageScope scope)
        {
            return await DbSet.Where(p => p.BizScope == scope && p.SubjectId == subjectId).ToListAsync();
        }
    }
}
