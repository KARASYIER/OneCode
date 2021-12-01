using OneCode.Application.Contracts;
using OneCode.BizImages.Dtos;
using OneCode.Domain;
using OneCode.Domain.Repositories;
using OneCode.Dtos;
using OneCode.EnumTypes;
using OneCode.Products.Dtos;
using OneCode.ToolKit.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace OneCode.Application
{
    public class ProductAppService : OneCodeAppService, IProductAppService
    {
        private IProductRepository _productRepository;
        private IShopProductRepository _shopProductRepository;
        private IBizImageRepository _BizImageRepository;
        private IShopRepository _shopRepository;

        public ProductAppService(
            IProductRepository productRepository,
            IShopProductRepository shopProductRepository,
            IBizImageRepository BizImageRepository,
            IShopRepository shopRepository
            )

        {
            _productRepository = productRepository;
            _BizImageRepository = BizImageRepository;
            _shopProductRepository = shopProductRepository;
            _shopRepository = shopRepository;
        }

        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ResponseReturn> CreateAsync(CreateProductInputDto input)
        {
            //优化: Guid通过工具类生成
            var product = new Product(GuidGenerator.Create())
            {
                IsOffShelf = false,
                IsSellOut = false,
                TypeName = input.TypeId.ToString()
            };

            ObjectMapper.Map(input, product);

            if (input.TypeId == ProductTypeEnum.酒店)
            {
                product.CommisionType = CommisionTypeEnum.Fixed;
                product.CommisionValue = input.CommisionValue;
                product.CommisionRate = 0.00M;
            }
            else
            {
                product.CommisionType = CommisionTypeEnum.Rate;
                product.CommisionRate = input.CommisionRate;
                product.CommisionValue = 0.00M;
            }

            //验证外部Id是否存在
            (await _productRepository.AnyAsync(p => p.OutterId == input.OutterId && !p.IsDeleted)).CheckBool("该产品已存在,无法重复添加");

            product.CheckProductTitle()
                   .CheckProductType()
                   .CheckCommisionRateAndValue();


            //获取最大排序号
            product.DisplayOrder = (await _productRepository.GetLastOrderAsync()) + 1;

            //更新数据
            await _productRepository.InsertAsync(product, true);

            return ResponseReturn.ReturnSuccess(
                data: ObjectMapper.Map<Product, ProductDto>(product)
                );
        }

        /// <summary>
        /// 逻辑删除产品
        /// </summary>
        public async Task<ResponseReturn> DeleteAsync(Guid id)
        {
            var product = (await _productRepository.FindAsync(id)).CheckNullOrDeleted();

            await _productRepository.DeleteAsync(product, true);

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 修改产品
        /// </summary>
        public async Task<ResponseReturn> UpdateAsync(Guid id, UpdateProductInputDto input)
        {
            var product = (await _productRepository.FindAsync(id)).CheckNullOrDeleted();

            ObjectMapper.Map(input, product);

            //修改产品,不再更新产品类型,关联的产品
            //(await _productRepository.AnyAsync(p => p.OutterId == input.OutterId && p.Id != product.Id && !p.IsDeleted)).CheckBool("无法查询到该此外部编号与类型相关的产品");

            product.CheckProductType()
                   .CheckCommisionRateAndValue();

            await _productRepository.UpdateAsync(product, true);

            //是否覆盖原有全部关联的店铺的佣金
            if (input.CoverCommision)
            {
                var shopProducts = await _shopProductRepository.GetListAsync(null, id, 1, 100000);

                shopProducts.ForEach(p =>
                {
                    if (p.CommisionType.IsFixed()) p.CommisionValue = product.CommisionValue;
                    if (p.CommisionType.IsRate()) p.CommisionRate = product.CommisionRate;
                });

                await _shopProductRepository.UpdateAsync(shopProducts);
            }

            return ResponseReturn.ReturnSuccess(
                data: ObjectMapper.Map<Product, ProductDto>(product)
                );
        }

        /// <summary>
        /// 修改产品销售状态
        /// </summary>
        public async Task<ResponseReturn> UpdateSaleStatusAsync(Guid id)
        {
            var product = (await _productRepository.FindAsync(id)).CheckNullOrDeleted();

            product.IsOffShelf = !product.IsOffShelf;

            await _productRepository.UpdateAsync(product, true);

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 修改产品库存状态
        /// </summary>
        public async Task<ResponseReturn> UpdateStockStatusAsync(Guid id)
        {
            var product = (await _productRepository.FindAsync(id)).CheckNullOrDeleted();

            product.IsSellOut = !product.IsSellOut;

            await _productRepository.UpdateAsync(product);

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 修改产品默认佣金率
        /// </summary>
        public async Task<ResponseReturn> UpdateBasicCommisionAsync(Guid id, UpdateCommisionRateOrValueInputDto input)
        {
            var product = (await _productRepository.FindAsync(id)).CheckNullOrDeleted();

            if (product.CommisionType.IsFixed())
            {
                product.CommisionValue = input.CommisionValue;
            }

            if (product.CommisionType.IsRate())
            {
                product.CommisionRate = input.CommisionRate;
            }

            product.CheckCommisionRateAndValue();

            await _productRepository.UpdateAsync(product);

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 关联到店铺
        /// </summary>
        public async Task<ResponseReturn> UpdateRelatedShopsAsync(Guid id, List<UpdateRelatedShopsInputDto> inputs)
        {
            var product = (await _productRepository.GetAsync(id)).CheckNullOrDeleted();

            foreach (var input in inputs)
            {
                var maxDisplayOrder = await _shopProductRepository.GetMaxDisplayOrderAsync(input.ShopId);

                (await _shopProductRepository.AnyAsync(p => p.ShopId == input.ShopId && p.ProductId == id)).CheckBool($"产品已关联到店铺,尝试刷新页面重新绑定");

                await _shopProductRepository.InsertAsync(new ShopProduct(input.ShopId, product.Id)
                {
                    CommisionType = product.CommisionType,
                    CommisionRate = product.CommisionType.IsFixed() ? 0.00M : product.CommisionRate,
                    CommisionValue = product.CommisionType.IsFixed() ? product.CommisionValue : 0.00M,
                    DisplayOrder = maxDisplayOrder + 1
                });
            }

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 取消关联
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        public async Task<ResponseReturn> UpdateUnrelatedShopsAsync(Guid id, List<Guid> shopIds)
        {
            foreach (var shopId in shopIds)
            {
                await _shopProductRepository.DeleteAsync(new ShopProduct(shopId, id));
            }

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 设置排序
        /// </summary>
        public async Task<ResponseReturn> UpdateDisplayOrderAsync(UpdateProductDisplayOrderInputDto input)
        {
            var changing = (await _productRepository.FindAsync(p => p.Id == input.Changing)).CheckNullOrDeleted();

            var target = (await _productRepository.FindAsync(p => p.Id == input.Target)).CheckNullOrDeleted();

            var tmep = changing.DisplayOrder;

            changing.DisplayOrder = target.DisplayOrder;

            target.DisplayOrder = tmep;

            await _productRepository.UpdateAsync(changing);

            await _productRepository.UpdateAsync(target);

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询单个产品
        /// </summary>
        public async Task<ResponseReturn> GetAsync(Guid id)
        {
            var product = (await _productRepository.FindAsync(p => p.Id == id)).CheckNullOrDeleted();

            return ResponseReturn.ReturnSuccess(
                data: ObjectMapper.Map<Product, ProductDto>(product)
                );
        }

        /// <summary>
        /// 查询产品集合
        /// </summary>
        public async Task<ResponseReturn> GetListAsync(GetProductsInputDto input)
        {
            var total = await _productRepository.GetCountAsync(input.Filter, input.ProductType, input.IsOffShelf, input.IsSellOut);

            var items = total == 0 ? new List<Product>() :
                    await _productRepository.GetListAsync(input.Filter, input.ProductType, input.IsOffShelf, input.IsSellOut, input.PageNo, input.PageSize);

            return ResponseReturn.ReturnSuccess(
                data: new PagedListResultDto<ProductDto>
                {
                    Items = ObjectMapper.Map<List<Product>, List<ProductDto>>(items),
                    TotalCount = total,
                    PageNo = input.PageNo,
                    PageSize = input.PageSize
                });

        }

        /// <summary>
        /// 获取已关联的店铺
        /// </summary>
        public async Task<ResponseReturn> GetRelatedShopsAsync(Guid id, PagedInputDto input)
        {
            var total = await _shopProductRepository.GetRelatedCountByProductIdAsync(id);

            //优化:数量为0,不重复查询
            var shopProducts = total == 0 ? new List<ShopProduct>() :
                 await _shopProductRepository.GetRelatedListByProductIdAsync(id, input.PageNo, input.PageSize);

            return ResponseReturn.ReturnSuccess(
                data: new PagedListResultDto<ProductRelatedShopDto>
                {
                    Items = ObjectMapper.Map<List<ShopProduct>, List<ProductRelatedShopDto>>(shopProducts),
                    PageNo = input.PageNo,
                    PageSize = input.PageSize,
                    TotalCount = total
                });
        }

        /// <summary>
        /// 获取未关联的店铺
        /// </summary>
        public async Task<ResponseReturn> GetUnrelatedShopsAsync(Guid id, PagedInputDto input)
        {
            var total = await _shopProductRepository.GetUnrelatedCountByProductIdAsync(id);

            var shops = total == 0 ? new List<Shop>() :
                await _shopProductRepository.GetUnrelatedListByProductIdAsync(id, input.PageNo, input.PageSize);

            return ResponseReturn.ReturnSuccess(
                data: new PagedListResultDto<ProductUnrelatedShopDto>
                {
                    Items = ObjectMapper.Map<List<Shop>, List<ProductUnrelatedShopDto>>(shops),
                    PageNo = input.PageNo,
                    PageSize = input.PageSize,
                    TotalCount = total
                });
        }
    }
}
