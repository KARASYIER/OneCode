using OneCode.Domain;
using OneCode.Domain.Repositories;
using OneCode.EntityFrameworkCore;
using OneCode.EnumTypes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OneCode.EntityFrameworkCore.Repositories
{
    public class DrawRepository : EfCoreRepository<OneCodeDbContext, Draw, Guid>, IDrawRepository
    {
        public DrawRepository(IDbContextProvider<OneCodeDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 新增一条提现
        /// </summary>
        public async Task CreateAsync(Draw draw, Shop shop)
        {
            DbContext.Draws.Add(draw);
            DbContext.Shops.Update(shop);

            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 提现审核通过
        /// </summary>
        /// <param name="draw"></param>
        /// <param name="shop"></param>
        /// <returns></returns>
        public async Task Approved(Draw draw, Shop shop, CommisionRecord record)
        {
            DbContext.Draws.Update(draw);
            DbContext.Shops.Update(shop);
            DbContext.CommisionRecords.Add(record);
            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 条件查询提现数量
        /// </summary>
        public async Task<long> GetCountAsync(string filter, Guid? shopId, DrawStatusEnum? status, DateTime? start, DateTime? end)
        {
            return await DbSet.AsNoTracking()
                              .WhereIf(shopId.HasValue, p => p.ShopId == shopId)
                              .WhereIf(start.HasValue, p => p.CreationTime >= start)
                              .WhereIf(end.HasValue, p => p.CreationTime < end.Value.Date.AddDays(1))
                              .WhereIf(status.HasValue, p => p.DrawStatus == status.Value)
                              .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.Name.Contains(filter) || p.Mobile.Contains(filter) || p.ShopName.Contains(filter))
                              .OrderByDescending(p => p.CreationTime)
                              .CountAsync();
        }

        /// <summary>
        /// 条件查询提现集合
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="shopId"></param>
        /// <param name="status"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<Draw>> GetListAsync(string filter, Guid? shopId, DrawStatusEnum? status, DateTime? start, DateTime? end, int pageIndex = 1, int pageSize = 20)
        {
            return await DbSet
                            .AsNoTracking()
                            .WhereIf(shopId.HasValue, p => p.ShopId == shopId)
                            .WhereIf(start.HasValue, p => p.CreationTime >= start)
                            .WhereIf(end.HasValue, p => p.CreationTime < end.Value.Date.AddDays(1))
                            .WhereIf(status.HasValue, p => p.DrawStatus == status.Value)
                            .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.Name.Contains(filter) || p.Mobile.Contains(filter) || p.ShopName.Contains(filter))
                            .OrderByDescending(p => p.CreationTime)
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

        }

        /// <summary>
        /// 查询单个店铺提取的累积佣金
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<decimal> GetDrewCommisionAsync(Guid shopId)
        {
            return await DbSet.AsNoTracking().Where(p => p.ShopId == shopId && p.DrawStatus == DrawStatusEnum.Approved)
                                             .SumAsync(p => p.Amount);
        }
    }
}
