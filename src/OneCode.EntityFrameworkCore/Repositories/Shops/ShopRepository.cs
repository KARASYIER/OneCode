using Microsoft.EntityFrameworkCore;
using OneCode.Domain;
using OneCode.Domain.Repositories;
using OneCode.EntityFrameworkCore;
using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OneCode.Repositories
{
    public class ShopRepository : EfCoreRepository<OneCodeDbContext, Shop, Guid>, IShopRepository
    {
        public ShopRepository(IDbContextProvider<OneCodeDbContext> dbContextProvider) : base(dbContextProvider) { }


        public async Task<Shop> GetByOwnerIdAsync(Guid ownerId)
        {
            return await DbSet.Where(p => p.OwnerId == ownerId)
                              .FirstOrDefaultAsync();
        }

        public async Task<long> GetCountAsync(string filter, Guid? ownerId, ShopStatusEnum? shopStatus)
        {
            return await DbSet.WhereIf(!string.IsNullOrWhiteSpace(filter), p =>
                                           p.Name.Contains(filter) ||
                                           p.Address.Contains(filter) ||
                                           p.Telephone.Contains(filter))
                              .WhereIf(ownerId.HasValue, p => p.OwnerId == ownerId)
                              .WhereIf(shopStatus.HasValue, p => p.Status == shopStatus)
                              .OrderByDescending(p => p.CreationTime)
                              .LongCountAsync();
        }

        public async Task<List<Shop>> GetListAsync(string filter, Guid? ownerId, ShopStatusEnum? shopStatus, int pageIndex = 0, int pageSize = 20)
        {
            return await DbSet.WhereIf(!string.IsNullOrWhiteSpace(filter), p =>
                                            p.Name.Contains(filter) ||
                                            p.Address.Contains(filter) ||
                                            p.Telephone.Contains(filter))
                              .WhereIf(ownerId.HasValue, p => p.OwnerId == ownerId)
                              .WhereIf(shopStatus.HasValue, p => p.Status == shopStatus)
                              .OrderByDescending(p => p.CreationTime)
                              .Skip((pageIndex - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public async Task<List<Shop>> GetListByTagIdAsync(Guid tagId)
        {
            return await DbContext.ShopTags.Where(p => p.TagId == tagId).Select(p => p.Shop).ToListAsync();

        }

        public override IQueryable<Shop> WithDetails()
        {
            return base.WithDetails().Include(x => x.Salers)
                                     .Include(x => x.ShopTags)
                                        .ThenInclude(t => t.Tag)
                                     .Include(x => x.ShopProducts)
                                        .ThenInclude(x => x.Product);
        }

        public Task<Shop> GetShopById(Guid id)
        {
            return DbSet.Where(p => p.Id == id)
                .Include(p => p.Products)
                .Where(p=>p.Products.Where(c=>!c.IsSellOut).Any())
                .FirstAsync();
        }

    }
}
