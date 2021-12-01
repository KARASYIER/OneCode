using OneCode.Domain;
using OneCode.Domain.Repositories;
using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace OneCode.Domain
{
    public class ShopManager : IDomainService
    {
        private IShopRepository _shopRepository;
        private IShopTagRepository _shopTagRepository;
        private IBizImageRepository _bizImagesRepository;
        private IProductRepository _productRepository;
        private IShopProductRepository _shopProductRepository;



        public ShopManager(
            IShopRepository shopRepository,
            IShopTagRepository shopTagRepository,
            IBizImageRepository bizImagesRepository,
            IProductRepository productRepository,
            IShopProductRepository shopProductRepository
            )
        {
            _shopRepository = shopRepository;
            _shopTagRepository = shopTagRepository;
            _bizImagesRepository = bizImagesRepository;
            _productRepository = productRepository;
            _shopProductRepository = shopProductRepository;
        }


        public async Task UpdateShopTagsAsync(Guid shopId, List<Guid> newTagIds)
        {
            var shop = await _shopRepository.GetAsync(shopId);

            var newShopTags = newTagIds.Select(p => new ShopTag(shopId, p)).ToList();

            await UpdateShopTagsAsync(shop, newShopTags);
        }


        public async Task UpdateShopTagsAsync(Shop shop, List<ShopTag> newShopTags)
        {
            var originShopTag = shop.ShopTags.Select(p => new ShopTag
            {
                ShopId = p.ShopId,
                TagId = p.TagId
            });

            foreach (var shopTag in originShopTag)
            {
                await _shopTagRepository.DeleteAsync(shopTag, true);
            }

            foreach (var shopTag in newShopTags)
            {
                await _shopTagRepository.InsertAsync(shopTag);
            }
        }

        public async Task DeleteAsync(Guid shopId)
        {
            var shop = await _shopRepository.GetAsync(shopId, true);

            //删除店铺相关的图片
            var bizImages = await _bizImagesRepository.GetListAsync(shopId, BizImageScope.Shop);

            foreach (var bizImage in bizImages)
            {
                await _bizImagesRepository.DeleteAsync(bizImage);
            }

            shop.ShopTags = null;
            shop.Salers = null;
            shop.ShopProducts = null;

            await _shopRepository.UpdateAsync(shop, true);
        }

        /// <summary>
        /// 在店铺下创建一个分销产品
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task CreateSingleSaleAsync(Guid shopId, Guid productId)
        {
            var product = await _productRepository.GetAsync(productId);

            var maxDisplayOrder = await _shopProductRepository.GetMaxDisplayOrderAsync(shopId);

            var shopProduct = new ShopProduct(shopId, productId)
            {
                CommisionRate = product.CommisionRate,
                DisplayOrder = maxDisplayOrder + 1
            };

            await _shopProductRepository.InsertAsync(shopProduct);
        }

        /// <summary>
        /// 在店铺下创建多个分销产品
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task CreateManaySaleAsync(Guid shopId, List<Guid> productIds)
        {
            var shop = await _shopRepository.GetAsync(shopId);

            var shopProducts = await _shopProductRepository.GetRelatedListByShopIdAsync(shopId);

            //复制一个原始的ShopProduct
            var retainShopProduct = shopProducts.Select(p =>
                                                            new ShopProduct(shop.Id, p.ProductId)
                                                            {
                                                                DisplayOrder = p.DisplayOrder,
                                                                CommisionRate = p.CommisionRate
                                                            })
                                                .ToList();

            //多对多关联表批量更新算法逻辑举例:
            //A   B   删   增
            //1       1
            //2   2       
            //3   3
            //4       4
            //5   5
            //    6        6
            //    7        7
            //检索A里不包含B的,1和4 删除
            //检索B里不包含A的,6和7 新增

            //检索A 删除该店铺下,不包含提交更新的分销关系
            foreach (var shopProduct in retainShopProduct.FindAll(p => !productIds.Contains(p.ProductId)))
            {
                await _shopProductRepository.DeleteAsync(shopProduct);
            }

            //检索B
            foreach (var productId in productIds.FindAll(p => !shopProducts.Select(sp => sp.ProductId).Contains(p)))
            {
                var product = await _productRepository.GetAsync(productId);

                var shopProduct = new ShopProduct
                {
                    ShopId = shop.Id,
                    ProductId = productId,
                    CommisionRate = product.CommisionRate,
                    DisplayOrder = (await _shopProductRepository.GetMaxDisplayOrderAsync(shopId)) + 1
                };

                await _shopProductRepository.InsertAsync(shopProduct);
            }
        }
    }
}
