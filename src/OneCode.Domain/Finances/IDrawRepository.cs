using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OneCode.Domain.Repositories
{
    /// <summary>
    /// 提现仓储
    /// </summary>
    public interface IDrawRepository : IRepository<Draw, Guid>
    {
        /// <summary>
        /// 新增一条提现
        /// </summary>
        /// <param name="draw"></param>
        /// <param name="shop"></param>
        /// <returns></returns>
        Task CreateAsync(Draw draw, Shop shop);

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="draw"></param>
        /// <param name="shop"></param>
        /// <returns></returns>
        Task Approved(Draw draw, Shop shop, CommisionRecord record);

        /// <summary>
        /// 条件查询提现记录数量
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="shopId"></param>
        /// <param name="status"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        Task<long> GetCountAsync(string filter, Guid? shopId, DrawStatusEnum? status, DateTime? start, DateTime? end);

        /// <summary>
        /// 条件查询提现记录集合
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="shopId"></param>
        /// <param name="status"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<Draw>> GetListAsync(string filter, Guid? shopId, DrawStatusEnum? status, DateTime? start, DateTime? end, int pageIndex = 1, int pageSize = 20);


        /// <summary>
        /// 查询单个店铺已提取的累计佣金
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<decimal> GetDrewCommisionAsync(Guid shopId);
    }
}
