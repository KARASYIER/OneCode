using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OneCode.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        /// <summary>
        /// 获取最后一个排序编号
        /// </summary>
        /// <returns></returns>
        Task<int> GetLastOrderAsync();

        /// <summary>
        /// 条件查询产品数量
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="productType"></param>
        /// <param name="isOffShelf"></param>
        /// <param name="isSellOut"></param>
        /// <returns></returns>
        Task<long> GetCountAsync(string filter, ProductTypeEnum? productType, bool? isOffShelf, bool? isSellOut);

        /// <summary>
        /// 条件查询产品
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="productType"></param>
        /// <param name="isOffShelf"></param>
        /// <param name="isSellOut"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<Product>> GetListAsync(string filter, ProductTypeEnum? productType, bool? isOffShelf, bool? isSellOut, int pageIndex, int pageSize);

        /// <summary>
        /// 根据店铺条件查询产品的数量
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="filter"></param>
        /// <param name="productType"></param>
        /// <param name="isOffShelf"></param>
        /// <param name="isSellOut"></param>
        /// <returns></returns>
        //Task<long> GetCountByShopIdAsync(Guid shopId, string filter, ProductTypeEnum? productType, bool? isOffShelf, bool? isSellOut);

        /// <summary>
        /// 根据店铺条件查询产品
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="filter"></param>
        /// <param name="productType"></param>
        /// <param name="isOffShelf"></param>
        /// <param name="isSellOut"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<ShopProduct>> GetListByShopIdAsync(Guid shopId); //, string filter, ProductTypeEnum? productType, bool? isOffShelf, bool? isSellOut, int pageIndex, int pageSize);

        /// <summary>
        /// 根据外部编号获取产品
        /// </summary>
        /// <param name="outterId"></param>
        /// <returns></returns>
        Task<Product> GetAsync(string outterId, ProductTypeEnum type);

    }
}
