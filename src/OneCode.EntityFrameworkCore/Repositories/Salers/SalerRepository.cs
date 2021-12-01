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
    public class SalerRepository : EfCoreRepository<OneCodeDbContext, Saler, Guid>, ISalerRepository
    {

        public SalerRepository(IDbContextProvider<OneCodeDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<Saler> GetByMobileAsync(string filter)
        {
            return await DbSet.Where(p => p.Mobile == (filter)).FirstOrDefaultAsync();
        }

        public async Task<long> GetCountAsync(string name, string mobile, string shopName, Guid? shopId, SalerTypeEnum? salerType, bool? salerStatus)
        {
            return await DbSet.Include(p => p.Shop)
                              .Where(p => p.IsDeleted.Equals(false))
                              .WhereIf(shopId.HasValue, p => p.ShopId == shopId)
                              .WhereIf(salerType.HasValue, p => p.Type == salerType)
                              .WhereIf(salerStatus.HasValue, p => p.Status == salerStatus)
                              .WhereIf(!string.IsNullOrWhiteSpace(name), p => p.Name.Contains(name))
                              .WhereIf(!string.IsNullOrWhiteSpace(mobile), p => p.Mobile.Contains(mobile))
                              .WhereIf(!string.IsNullOrWhiteSpace(shopName), p => p.Shop.Name.Contains(shopName))
                              .OrderByDescending(p => p.CreationTime)

                              .LongCountAsync();
        }

        public async Task<List<Saler>> GetListAsync(string name, string mobile, string shopName, Guid? shopId, SalerTypeEnum? salerType, bool? salerStatus, int pageNo = 1, int pageSize = 20)
        {
            return await DbSet.Include(p => p.Shop)
                              .Where(p => p.IsDeleted.Equals(false))
                              .WhereIf(shopId.HasValue, p => p.ShopId == shopId)
                              .WhereIf(salerType.HasValue, p => p.Type == salerType)
                              .WhereIf(salerStatus.HasValue, p => p.Status == salerStatus)
                              .WhereIf(!string.IsNullOrWhiteSpace(name), p => p.Name.Contains(name))
                              .WhereIf(!string.IsNullOrWhiteSpace(mobile), p => p.Mobile.Contains(mobile))
                              .WhereIf(!string.IsNullOrWhiteSpace(shopName), p => p.Shop.Name.Contains(shopName))
                              .OrderByDescending(p => p.CreationTime)
                              .Skip((pageNo - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public async Task<List<Saler>> GetListAsync(Guid shopId)
        {
            return await DbSet.Where(p => p.ShopId == shopId && p.IsDeleted == false).ToListAsync();
        }
    }
}
