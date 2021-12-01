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
    public class ShopTagRepository : EfCoreRepository<OneCodeDbContext, ShopTag>, IShopTagRepository
    {
        public ShopTagRepository(IDbContextProvider<OneCodeDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<ShopTag>> GetListByShopId(Guid shopId)
        {
            return await DbSet.Where(p => p.ShopId == shopId).ToListAsync();
        }

        public async Task<List<ShopTag>> GetListByTagId(Guid tagId)
        {
            return await DbSet.Where(p => p.TagId == tagId).ToListAsync();
        }

        //public override IQueryable<ShopTag> WithDetails()
        //{
        //    return base.WithDetails().Include(x => x.Tag).Include(x => x.Shop);
        //}
    }
}
