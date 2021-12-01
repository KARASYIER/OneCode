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
    public class OrderRepository : EfCoreRepository<OneCodeDbContext, Order, Guid>, IOrderRepository
    {
        public OrderRepository(IDbContextProvider<OneCodeDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        public async Task CreateOrderAsync(Order order, Shop shop)
        {
            DbContext.Orders.Add(order);

            DbContext.Shops.Update(shop);

            await DbContext.SaveChangesAsync();

        }

        /// <summary>
        /// 部分退款
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderDetails"></param>
        /// <param name="shop"></param>
        /// <returns></returns>
        public async Task RefundPartailAsync(Order order, List<OrderDetail> orderDetails, Shop shop)
        {
            DbSet.Update(order);

            DbContext.OrderDetails.AddRange(orderDetails);

            DbContext.Shops.Update(shop);

            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="order"></param>
        /// <param name="commisionRecord"></param>
        /// <returns></returns>
        public async Task FinishAsync(Order order, Shop shop, CommisionRecord commisionRecord)
        {
            DbContext.Orders.Update(order);
            DbContext.Shops.Update(shop);
            DbContext.CommisionRecords.Add(commisionRecord);
            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 根据外部订单号查询
        /// </summary>
        /// <param name="outterId"></param>
        /// <returns></returns>
        public async Task<Order> GetByOutterIdAsync(string outterId)
        {
            return await DbSet.Include(p => p.OrderDetails).FirstOrDefaultAsync(p => p.OutterOrderId == outterId);
        }

        /// <summary>
        /// 条件查询数量
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="outterOrderId"></param>
        /// <param name="shopId"></param>
        /// <param name="salerId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="orderStatus"></param>
        /// <param name="orderBizStatus"></param>
        /// <returns></returns>
        public async Task<long> GetCountAsync(string filter, string outterOrderId, Guid? shopId, Guid? salerId, DateTime? startDate, DateTime? endDate, OrderStatus? orderStatus, OrderBizStatus? orderBizStatus)
        {
            return await DbSet.AsNoTracking()
                              .WhereIf(!string.IsNullOrWhiteSpace(outterOrderId), p => p.OutterOrderId == outterOrderId)
                              .WhereIf(orderStatus.HasValue, p => p.OrderStatus == orderStatus)
                              .WhereIf(orderBizStatus.HasValue, p => p.BizStatus == orderBizStatus)
                              .WhereIf(shopId.HasValue, p => p.ShopId.Equals(shopId))
                              .WhereIf(salerId.HasValue, p => p.SalerId.Equals(salerId))
                              .WhereIf(startDate.HasValue, p => p.CreationTime >= startDate)
                              .WhereIf(endDate.HasValue, p => p.CreationTime < endDate.Value.Date.AddDays(1))
                              .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.Title.Contains(filter))
                              .OrderByDescending(p => p.CreationTime)
                              .CountAsync();
        }

        /// <summary>
        /// 条件查询集合
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="outterOrderId"></param>
        /// <param name="shopId"></param>
        /// <param name="salerId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="orderStatus"></param>
        /// <param name="orderBizStatus"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<Order>> GetListAsync(string filter, string outterOrderId, Guid? shopId, Guid? salerId, DateTime? startDate, DateTime? endDate, OrderStatus? orderStatus, OrderBizStatus? orderBizStatus, int pageIndex = 1, int pageSize = 20)
        {
            return await DbSet.AsNoTracking()
                              .Include(p => p.OrderDetails)
                              .WhereIf(!string.IsNullOrWhiteSpace(outterOrderId), p => p.OutterOrderId == outterOrderId)
                              .WhereIf(orderStatus.HasValue, p => p.OrderStatus == orderStatus)
                              .WhereIf(orderBizStatus.HasValue, p => p.BizStatus == orderBizStatus)
                              .WhereIf(shopId.HasValue, p => p.ShopId.Equals(shopId))
                              .WhereIf(salerId.HasValue, p => p.SalerId.Equals(salerId))
                              .WhereIf(startDate.HasValue, p => p.CreationTime >= startDate)
                              .WhereIf(endDate.HasValue, p => p.CreationTime < endDate.Value.Date.AddDays(1))
                              .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.Title.Contains(filter))
                              .OrderByDescending(p => p.CreationTime)
                              .Skip((pageIndex - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public override IQueryable<Order> WithDetails()
        {
            return base.WithDetails().Include(x => x.OrderDetails);
        }


        /// <summary>
        /// 获取累计佣金
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<decimal> GetAccumulatedCommisionAsync(Guid shopId, Guid? salerId, bool isOwner = false)
        {
            return await DbSet.AsNoTracking()
                              .Where(p => p.ShopId == shopId)
                              .WhereIf(salerId.HasValue && isOwner == false, p => p.SalerId == salerId)
                              .SumAsync(p => p.TotalCommision);
        }

        /// <summary>
        /// 查询当日获得的佣金
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<decimal> GetTodayCommisionAsync(Guid shopId, Guid? salerId)
        {
            return await DbSet.AsNoTracking()
                              .Where(p => p.ShopId == shopId && p.CreationTime >= DateTime.Now.Date && p.CreationTime < DateTime.Now.AddDays(1).Date)
                              .WhereIf(salerId.HasValue, p => p.SalerId == salerId)
                              .SumAsync(p => p.TotalCommision);
        }

    }
}
