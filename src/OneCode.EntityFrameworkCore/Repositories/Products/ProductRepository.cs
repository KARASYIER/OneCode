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
    public class ProductRepository : EfCoreRepository<OneCodeDbContext, Product, Guid>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<OneCodeDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<int> GetLastOrderAsync()
        {
            return (await DbSet.OrderByDescending(p => p.DisplayOrder).FirstOrDefaultAsync())?.DisplayOrder ?? 0;
        }

        public async Task<Product> GetAsync(string outterId, ProductTypeEnum type)
        {
            return await DbSet.FirstOrDefaultAsync(p => p.OutterId.Equals(outterId) && p.TypeId.Equals(type));
        }

        public async Task<long> GetCountAsync(string filter, ProductTypeEnum? productType, bool? isOffShelf, bool? isSellOut)
        {
            return await DbSet.WhereIf(productType.HasValue, p => p.TypeId == productType)
                              .WhereIf(isOffShelf.HasValue, p => p.IsOffShelf == isOffShelf)
                              .WhereIf(isSellOut.HasValue, p => p.IsSellOut == isSellOut)
                              .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.Title.Contains(filter))
                              .CountAsync();
        }

        public async Task<List<Product>> GetListAsync(string filter, ProductTypeEnum? productType, bool? isOffShelf, bool? isSellOut, int pageIndex, int pageSize)
        {
            return await DbSet.Where(p => !p.IsDeleted)
                              .WhereIf(productType.HasValue, p => p.TypeId == productType)
                              .WhereIf(isOffShelf.HasValue, p => p.IsOffShelf == isOffShelf)
                              .WhereIf(isSellOut.HasValue, p => p.IsSellOut == isSellOut)
                              .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.Title.Contains(filter))
                              .OrderByDescending(p => p.DisplayOrder)
                              .Skip((pageIndex - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }


        public async Task<long> GetCountByShopIdAsync(Guid shopId, string filter, ProductTypeEnum? productType, bool? isOffShelf, bool? isSellOut)
        {
            return await (from p in DbContext.Products
                          join sp in DbContext.ShopProducts on p.Id equals sp.ProductId into sss
                          from s in sss.DefaultIfEmpty()
                          where p.IsDeleted == false && s.ShopId == shopId
                          select new { p, s })
                          .Where(p => p.s.ShopId == shopId)
                          .WhereIf(productType.HasValue, p => p.p.TypeId == productType)
                          .WhereIf(isOffShelf.HasValue, p => p.p.IsOffShelf == isOffShelf)
                          .WhereIf(isSellOut.HasValue, p => p.p.IsSellOut == isSellOut)
                          .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.p.Title.Contains(filter))

                          .CountAsync();
        }


        public async Task<List<ShopProduct>> GetListByShopIdAsync(Guid shopId)
        {
            return await DbContext.ShopProducts.Include(p => p.Product)
                                               .Where(p => p.ShopId == shopId)
                                               .ToListAsync();

            //return await (from p in DbContext.Products
            //              join sp in DbContext.ShopProducts on p.Id equals sp.ProductId into sss
            //              from s in sss.DefaultIfEmpty()
            //              where p.IsDeleted == false && s.ShopId == shopId
            //              select new { p, s })
            //                .WhereIf(shopId.HasValue, p => p.s.ShopId == shopId)
            //                .WhereIf(productType.HasValue, p => p.p.TypeId == productType)
            //                .WhereIf(isOffShelf.HasValue, p => p.p.IsOffShelf == isOffShelf)
            //                .WhereIf(isSellOut.HasValue, p => p.p.IsSellOut == isSellOut)
            //                .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.p.Title.Contains(filter))
            //                .CountAsync();
        }


        public override IQueryable<Product> WithDetails()
        {
            return base.WithDetails().Include(x => x.ShopProducts)
                                        .ThenInclude(x => x.Shop);
        }
    }
}
