using Microsoft.EntityFrameworkCore;
using OneCode.Domain;
using OneCode.Domain.Repositories;
using OneCode.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OneCode.Repositories
{
    public class ShopProductRepository : EfCoreRepository<OneCodeDbContext, ShopProduct>, IShopProductRepository
    {
        public ShopProductRepository(IDbContextProvider<OneCodeDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<ShopProduct> GetAsync(Guid shopId, Guid productId)
        {
            return await DbSet.FirstAsync(p => p.ProductId.Equals(productId) && p.ShopId.Equals(shopId));
        }

        #region 从店铺获取关联的产品
        public async Task<long> GetRelatedCountByShopIdAsync(Guid shopId)
        {
            return await DbSet.Include(p => p.Product)
                              .Where(p => p.ShopId == shopId && p.Product.IsDeleted == false)
                              .CountAsync();
        }

        public async Task<List<ShopProduct>> GetRelatedListByShopIdAsync(Guid shopId, int pageNo = 1, int pageSize = 20)
        {
            return await DbSet.Include(p => p.Product)
                              .Where(p => p.ShopId == shopId && p.Product.IsDeleted == false)
                              .Skip((pageNo - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public async Task<long> GetUnrelatedCountByShopIdAsync(Guid shopId)
        {
            var lst = await DbContext.ShopProducts.Where(x => x.ShopId == shopId).Select(x => x.ProductId).ToListAsync();

            return await DbContext.Products.Where(p => p.IsDeleted == false && !lst.Contains(p.Id))
                                           .CountAsync();
        }

        public async Task<List<Product>> GetUnrelatedListByShopIdAsync(Guid shopId, int pageNo = 1, int pageSize = 20)
        {
            var lst = await DbContext.ShopProducts.Where(x => x.ShopId == shopId).Select(x => x.ProductId).ToListAsync();

            return await DbContext.Products.Where(p => p.IsDeleted == false && !lst.Contains(p.Id))
                                           .Skip((pageNo - 1) * pageSize)
                                           .Take(pageSize)
                                           .ToListAsync();
        }

        #endregion

        #region 从产品获取关联的店铺
        public async Task<long> GetRelatedCountByProductIdAsync(Guid productId)
        {
            return await DbSet.Include(p => p.Shop)
                              .Include(p => p.Product)
                              .Where(p => p.ProductId == productId && p.Product.IsDeleted == false)
                              .CountAsync();
        }

        public async Task<List<ShopProduct>> GetRelatedListByProductIdAsync(Guid productId, int pageNo = 1, int pageSize = 20)
        {
            return await DbSet.Include(p => p.Shop)
                              .Include(p => p.Product)
                              .Where(p => p.ProductId == productId && p.Product.IsDeleted == false)
                              .Skip((pageNo - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public async Task<long> GetUnrelatedCountByProductIdAsync(Guid productId)
        {
            var shopIds = await DbContext.ShopProducts.Where(x => x.ProductId == productId).Select(x => x.ShopId).ToListAsync();

            return await DbContext.Shops.Where(p => p.IsDeleted == false && !shopIds.Contains(p.Id))
                                           .CountAsync();
        }

        /// <summary>
        /// 某个产品未关联的店铺
        /// </summary>
        public async Task<List<Shop>> GetUnrelatedListByProductIdAsync(Guid productId, int pageNo = 1, int pageSize = 20)
        {
            var shopIds = await DbContext.ShopProducts.Where(x => x.ProductId == productId).Select(x => x.ShopId).ToListAsync();

            return await DbContext.Shops
                                           .Where(p => p.IsDeleted == false && !shopIds.Contains(p.Id))
                                           .Skip((pageNo - 1) * pageSize)
                                           .Take(pageSize)
                                           .ToListAsync();
        }
        #endregion

        public async Task<long> GetCountByProductIdAsync(Guid productId)
        {
            return await DbSet.Include(p => p.Shop)
                              .Where(p => p.ProductId == productId && p.Shop.IsDeleted == false)
                              .CountAsync();
        }

        public async Task<List<Shop>> GetListByProductIdAsync(Guid productId, int pageIndex = 1, int pageSize = 20)
        {
            var list = await DbSet.Include(p => p.Shop)
                                  .Where(p => p.ProductId == productId && p.Shop.IsDeleted == false)
                                  .Select(p => p.Shop)
                                  .ToListAsync();

            return list;
        }

        public async Task<int> GetMaxDisplayOrderAsync(Guid shopId)
        {
            return (await DbSet.Where(p => p.ShopId == shopId)
                               .OrderByDescending(p => p.DisplayOrder)
                               .FirstOrDefaultAsync())?.DisplayOrder ?? 0;
        }

        public async Task<List<ShopProduct>> GetListAsync(Guid? shopId, Guid? productId, int pageIndex = 1, int pageSize = 20)
        {
            var list = await DbSet
                                  .WhereIf(shopId.HasValue, p => p.ShopId == shopId)
                                  .WhereIf(productId.HasValue, p => p.ProductId == productId)
                                  .PageBy((pageIndex - 1) * pageSize, pageSize)
                                  .ToListAsync();

            return list;


        }

        public async Task UpdateAsync(List<ShopProduct> shopProducts)
        {
            DbSet.UpdateRange(shopProducts);

            await DbContext.SaveChangesAsync();
        }
    }
}
