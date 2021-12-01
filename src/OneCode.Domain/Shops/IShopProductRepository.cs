using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OneCode.Domain.Repositories
{
    public interface IShopProductRepository : IRepository<ShopProduct>
    {
        /// <summary>
        /// 获取单个分销关系
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<ShopProduct> GetAsync(Guid shopId, Guid productId);

        #region 获取店铺下的关系产品
        /// <summary>
        /// 获取店铺下已关联的产品数量
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<long> GetRelatedCountByShopIdAsync(Guid shopId);

        /// <summary>
        /// 获取店铺下已关联的产品
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<ShopProduct>> GetRelatedListByShopIdAsync(Guid shopId, int pageNo = 1, int pageSize = 20);

        /// <summary>
        /// 获取店铺下未关联的产品数量
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<long> GetUnrelatedCountByShopIdAsync(Guid shopId);

        /// <summary>
        /// 获取店铺下未关联的产品
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        Task<List<Product>> GetUnrelatedListByShopIdAsync(Guid shopId, int pageNo = 1, int pageSize = 20);
        #endregion

        #region 从产品获取店铺关系
        /// <summary>
        /// 获取店铺下已关联的产品数量
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<long> GetRelatedCountByProductIdAsync(Guid productId);

        /// <summary>
        /// 获取店铺下已关联的产品
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<ShopProduct>> GetRelatedListByProductIdAsync(Guid productId, int pageNo = 1, int pageSize = 20);

        /// <summary>
        /// 获取店铺下未关联的产品数量
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<long> GetUnrelatedCountByProductIdAsync(Guid productId);

        /// <summary>
        /// 获取店铺下未关联的产品
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        Task<List<Shop>> GetUnrelatedListByProductIdAsync(Guid productId, int pageNo = 1, int pageSize = 20);
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<long> GetCountByProductIdAsync(Guid productId);

        /// <summary>
        /// 根据产品获取分销关系(暂不分页,保留分页参数)
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<Shop>> GetListByProductIdAsync(Guid productId, int pageNo = 1, int pageSize = 20);

        /// <summary>
        /// 获取当前店铺下产品的最大序号
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<int> GetMaxDisplayOrderAsync(Guid shopId);

        Task<List<ShopProduct>> GetListAsync(Guid? shopId, Guid? productId, int pageIndex = 1, int pageSize = 20);

        Task UpdateAsync(List<ShopProduct> shopProducts);
    }
}
