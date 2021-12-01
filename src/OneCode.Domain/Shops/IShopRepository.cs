using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OneCode.Domain.Repositories
{
    public interface IShopRepository : IRepository<Shop, Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        Task<Shop> GetByOwnerIdAsync(Guid ownerId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="ownerId"></param>
        /// <param name="shopStatus"></param>
        /// <returns></returns>
        Task<long> GetCountAsync(string filter, Guid? ownerId, ShopStatusEnum? shopStatus);

        /// <summary>
        /// /
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="ownerId"></param>
        /// <param name="shopStatus"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<Shop>> GetListAsync(string filter, Guid? ownerId, ShopStatusEnum? shopStatus, int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        Task<List<Shop>> GetListByTagIdAsync(Guid tagId);

        Task<Shop> GetShopById(Guid id);
    }
}
