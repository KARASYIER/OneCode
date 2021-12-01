using Microsoft.EntityFrameworkCore;
using OneCode.Domain;
using OneCode.Domain.Repositories;
using OneCode.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OneCode.Repositories
{
    public class TagRepository : EfCoreRepository<OneCodeDbContext, Tag, Guid>, ITagRepository
    {
        public TagRepository(IDbContextProvider<OneCodeDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 根据名称获取Tag
        /// </summary>
        public async Task<Tag> GetByNameAsync(string tagName)
        {
            return await DbSet.FirstOrDefaultAsync(p => p.Name.Equals(tagName) && p.IsDeleted == false);
        }


        public async Task<long> GetCountAsync(string filter)
        {
            return await DbSet.WhereIf(!string.IsNullOrEmpty(filter), p => p.Name.Contains(filter))
                              .CountAsync();
        }

        public async Task<List<Tag>> GetListAsync(string filter, int pageNo = 1, int pageSize = 20)
        {
            return await DbSet.WhereIf(!string.IsNullOrEmpty(filter), p => p.Name.Contains(filter))
                              .OrderByDescending(p => p.CreationTime)
                              .Skip((pageNo - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public override IQueryable<Tag> WithDetails()
        {
            return base.WithDetails().Include(x => x.ShopTags);
        }
    }
}
