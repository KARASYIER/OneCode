using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OneCode.Application.Contracts;
using OneCode.Domain;
using OneCode.Domain.Repositories;
using OneCode.Dtos;
using OneCode.EnumTypes;
using OneCode.Products.Dtos;
using OneCode.Salers.Dtos;
using OneCode.Shops.Dtos;
using OneCode.Tags.Dtos;
using OneCode.ToolKit.Http;
using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OneCode.Application
{

    public class ShopAppService : OneCodeAppService, IShopAppService
    {
        private IOptions<OneCodeSettingOptions> _oneCodeSetting;
        private IHostEnvironment _webHostEnvironment;

        private IShopRepository _shopRepository;
        private IShopTagRepository _shopTagRepository;
        private IShopProductRepository _shopProductRepository;
        private ISalerRepository _salerRepository;
        private ITagRepository _tagRepository;
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;
        private IDrawRepository _drawRepository;

        public ShopAppService(
            IOptions<OneCodeSettingOptions> oneCodeSetting,
            IWebHostEnvironment webHostEnvironment,

            IShopRepository shopRepository,
            IShopTagRepository shopTagRepository,
            IShopProductRepository shopProductRepository,
            ISalerRepository salerRepository,
            ITagRepository tagRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IDrawRepository drawRepository
            )
        {
            _oneCodeSetting = oneCodeSetting;
            _webHostEnvironment = webHostEnvironment;

            _shopRepository = shopRepository;
            _shopTagRepository = shopTagRepository;
            _salerRepository = salerRepository;
            _shopProductRepository = shopProductRepository;
            _tagRepository = tagRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _drawRepository = drawRepository;
        }

        /// <summary>
        /// 创建店铺
        /// </summary>
        public async Task<ResponseReturn> CreateAsync(CreateOrUpdateShopInputDto input)
        {
            var id = GuidGenerator.Create();

            var shop = new Shop(id)
            {
                Status = ShopStatusEnum.Openning,
                QRCodeUrl = GeneratorQRCode($"{_oneCodeSetting.Value.OneCodeQR}?shopid={id}")
            };

            //二维码


            ObjectMapper.Map(input, shop);


            foreach (var tag in input.tags)
            {
                if ((await _tagRepository.GetAsync(tag.Id)) != null)
                {
                    shop.ShopTags.Add(new ShopTag(shop.Id, tag.Id));
                }
            }

            await _shopRepository.InsertAsync(shop);

            return ResponseReturn.ReturnSuccess(
                data: ObjectMapper.Map<Shop, ShopFullDto>(await _shopRepository.GetAsync(shop.Id))
                );
        }

        /// <summary>
        /// 删除店铺
        /// 
        /// </summary>
        public async Task<ResponseReturn> DeleteAsync(Guid id)
        {
            var shop = (await _shopRepository.FindAsync(id)).CheckNullOrDeleted();

            //目前逻辑不删除其关联的分销员,关联的产品
            await _shopRepository.DeleteAsync(shop);

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 待优化级联关系的修改
        /// </summary>
        public async Task<ResponseReturn> UpdateAsync(Guid id, CreateOrUpdateShopInputDto input)
        {
            var shop = (await _shopRepository.FindAsync(id)).CheckNullOrDeleted();

            ObjectMapper.Map(input, shop);

            //清除原始关联的Tag
            foreach (var shopTag in shop.ShopTags.ToList())
            {
                await _shopTagRepository.DeleteAsync(shopTag);
            }

            //关联新Tag
            if (!input.tags.IsNullOrEmpty())
            {
                foreach (var tag in input.tags)
                {
                    if (await _tagRepository.AnyAsync(p => p.Id == tag.Id && !p.IsDeleted))
                    {
                        await _shopTagRepository.InsertAsync(new ShopTag(id, tag.Id));
                    }
                }
            }

            //更新
            await _shopRepository.UpdateAsync(shop, true);

            return ResponseReturn.ReturnSuccess(
                data: ObjectMapper.Map<Shop, ShopFullDto>(await _shopRepository.GetAsync(shop.Id))
                );
        }

        /// <summary>
        /// 更新店铺名称
        /// </summary>
        public async Task<ResponseReturn> UpdateShopNameAsync(Guid id, string shopName)
        {
            var shop = (await _shopRepository.FindAsync(id, false)).CheckNullOrDeleted();

            shop.Name = shopName;

            shop.CheckName();

            await _shopRepository.UpdateAsync(shop);

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public async Task<ResponseReturn> UpdateStatusAsync(Guid id, ShopStatusEnum shopStatus)
        {
            var shop = (await _shopRepository.FindAsync(id, false)).CheckNullOrDeleted();

            shop.Status = shopStatus;

            await _shopRepository.UpdateAsync(shop);

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 更新负责人
        /// </summary>
        public async Task<ResponseReturn> UpdateOwnerAsync(Guid id, Guid salerId)
        {
            var shop = (await _shopRepository.FindAsync(id, false)).CheckNullOrDeleted();

            var saler = (await _salerRepository.FindAsync(p => p.Id == salerId && p.ShopId == id)).CheckNullOrDeleted("该分销员不存在可能已删除,或者该分销员不属于该店铺");

            shop.OwnerId = salerId;
            shop.OwnerName = saler.Name;

            await _shopRepository.UpdateAsync(shop);

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 更新佣金比例
        /// </summary>
        public async Task<ResponseReturn> UpdateCommisionRateAsync(Guid id, UpdateCommisionRateInputDto input)
        {
            var shopProduct = (await _shopProductRepository.FindAsync(p => p.ShopId == id && p.ProductId == input.ProductId, false)).CheckNull("没有查询到店铺与该产品关联数据,请先设置关联后再调整佣金");

            shopProduct.CommisionRate = input.CommisionRate;

            await _shopProductRepository.UpdateAsync(shopProduct);

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 设置关联
        /// </summary>
        public async Task<ResponseReturn> UpdateRelatedProductsAsync(Guid id, List<UpdateRelatedProductsInputDto> inputs)
        {
            (!await _shopRepository.AnyAsync(p => p.Id == id && p.IsDeleted == false)).CheckBool();

            foreach (var input in inputs)
            {
                var product = await _productRepository.FindAsync(input.ProductId);

                var maxDisplayOrder = await _shopProductRepository.GetMaxDisplayOrderAsync(id);

                await _shopProductRepository.InsertAsync(new ShopProduct(id, product.Id)
                {
                    CommisionType = product.CommisionType,
                    CommisionRate = product.CommisionType.IsFixed() ? 0.00M : product.CommisionValue,
                    CommisionValue = product.CommisionType.IsFixed() ? product.CommisionValue : 0.00M,

                    DisplayOrder = maxDisplayOrder + 1
                });
            }

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 取消关联
        /// </summary>
        public async Task<ResponseReturn> UpdateUnrelatedProductsAsync(Guid id, List<UpdateRelatedProductsInputDto> inputs)
        {
            foreach (var productId in inputs)
            {
                await _shopProductRepository.DeleteAsync(new ShopProduct(id, productId.ProductId));
            }

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 设置排序
        /// </summary>
        public async Task<ResponseReturn> UpdateDisplayOrderAsync(Guid id, Shops.Dtos.UpdateDisplayOrderInputDto input)
        {
            var changing = (await _shopProductRepository.FindAsync(p => p.ShopId == id && p.ProductId == input.Changing)).CheckNull();

            var target = (await _shopProductRepository.FindAsync(p => p.ShopId == id && p.ProductId == input.Target)).CheckNull();

            var tmep = changing.DisplayOrder;

            changing.DisplayOrder = target.DisplayOrder;

            target.DisplayOrder = tmep;

            await _shopProductRepository.UpdateAsync(changing);

            await _shopProductRepository.UpdateAsync(target);

            return ResponseReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseReturn> GetAsync(Guid id)
        {
            var shop = (await _shopRepository.FindAsync(id)).CheckNullOrDeleted();

            return ResponseReturn.ReturnSuccess(
                data: ObjectMapper.Map<Shop, ShopFullDto>(shop)
                );
        }

        /// <summary>
        /// 查询集合
        /// </summary>
        public async Task<ResponseReturn> GetListAsync(GetShopsInputDto input)
        {
            var total = await _shopRepository.GetCountAsync(input.filter, input.OwnerId, input.ShopStatus);

            var shops = await _shopRepository.GetListAsync(
                            input.filter,
                            input.OwnerId,
                            input.ShopStatus,
                            input.PageNo,
                            input.PageSize);

            return ResponseReturn.ReturnSuccess(
               data: new PagedListResultDto<ShopDto>
               {
                   Items = ObjectMapper.Map<List<Shop>, List<ShopDto>>(shops),
                   TotalCount = total,
                   PageNo = input.PageNo,
                   PageSize = input.PageSize
               });
        }

        /// <summary>
        /// 根据产品查询集合
        /// </summary>
        public async Task<ResponseReturn> GetListByProductIdAsync(Guid productId)
        {
            var shops = await _shopProductRepository.GetListByProductIdAsync(productId);

            return ResponseReturn.ReturnSuccess(
                data: ObjectMapper.Map<List<Shop>, List<ShopDto>>(shops)
                );
        }

        /// <summary>
        /// 根据标签查询集合
        /// </summary>
        public async Task<ResponseReturn> GetListByTagIdAsync(Guid tagId)
        {
            var shops = await _shopRepository.GetListByTagIdAsync(tagId);

            return ResponseReturn.ReturnSuccess(
                data: ObjectMapper.Map<List<Shop>, List<ShopDto>>(shops)
                 );
        }

        /// <summary>
        /// 获取该店铺下的分销员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseReturn> GetSalersAsync(Guid id, PagedInputDto input)
        {
            var totalCount = await _salerRepository.GetCountAsync(null, null, null, id, null, null);

            var salers = await _salerRepository.GetListAsync(null, null, null, id, null, null, input.PageNo, input.PageSize);

            return ResponseReturn.ReturnSuccess(
                data: new PagedListResultDto<SalerDto>
                {
                    Items = ObjectMapper.Map<List<Saler>, List<SalerDto>>(salers),
                    TotalCount = totalCount,
                    PageNo = input.PageNo,
                    PageSize = input.PageSize
                });
        }

        /// <summary>
        /// 获取状态枚举
        /// </summary>
        public Task<ResponseReturn> GetStatusOptionsAsync()
        {
            return ResponseReturn.ReturnSuccessAsync(
                data: ReturnOptionListResult(typeof(ShopStatusEnum))
                );
        }

        /// <summary>
        /// 获取已关联的产品
        /// </summary>
        public async Task<ResponseReturn> GetRelatedProductsAsync(Guid id, PagedInputDto input)
        {
            var total = await _shopProductRepository.GetRelatedCountByShopIdAsync(id);

            var shopProducts = await _shopProductRepository.GetRelatedListByShopIdAsync(id, input.PageNo, input.PageSize);

            return ResponseReturn.ReturnSuccess(
                data: new PagedListResultDto<ShopRelatedProductDto>
                {
                    Items = ObjectMapper.Map<List<ShopProduct>, List<ShopRelatedProductDto>>(shopProducts),
                    PageNo = input.PageNo,
                    PageSize = input.PageSize,
                    TotalCount = total
                });
        }

        /// <summary>
        /// 获取未关联的产品
        /// </summary>
        public async Task<ResponseReturn> GetUnrelatedProductsAsync(Guid id, PagedInputDto input)
        {
            var total = await _shopProductRepository.GetUnrelatedCountByShopIdAsync(id);

            var products = await _shopProductRepository.GetUnrelatedListByShopIdAsync(id, input.PageNo, input.PageSize);

            return ResponseReturn.ReturnSuccess(
                data: new PagedListResultDto<ShopUnrelatedProductDto>
                {
                    Items = ObjectMapper.Map<List<Product>, List<ShopUnrelatedProductDto>>(products),
                    PageNo = input.PageNo,
                    PageSize = input.PageSize,
                    TotalCount = total
                }
                );
        }

        /// <summary>
        /// 获取全店铺全产品关系结构,测试用
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseReturn> GetFullAsync()
        {
            var shops = await _shopRepository.GetListAsync(true);
            return ResponseReturn.ReturnSuccess(
                data: ObjectMapper.Map<List<Shop>, List<ShopFullTestDto>>(shops)
                );
        }

        /// <summary>
        /// 前端获取店铺关联的产品(包括店铺信息)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseReturn> GetShopWithProductsAsync(Guid id)
        {
            var shop = await _shopRepository.FindAsync(id, true);
            //var shop = await _shopRepository.GetShopById(id);

            var h5ShopDto = ObjectMapper.Map<Shop, H5ShopWithProductsDto>(shop);

            h5ShopDto.Products = ObjectMapper.Map<ICollection<ShopProduct>, List<H5ProductDto>>(shop.ShopProducts);


            h5ShopDto.Products.ForEach(p =>
            {
                string url = string.Empty;
                switch (p.TypeId)
                {
                    case ProductTypeEnum.车票:
                        url = _oneCodeSetting.Value.ProductUrlFormat_Bus;

                        url = url.Replace("{CityStart}", p.CityStart)
                                 .Replace("{CityEnd}", p.CityEnd)
                                 .Replace("{DriveDate}", DateTime.Now.ToString("yyyy-MM-dd"));

                        break;

                    case ProductTypeEnum.酒店:

                        url = _oneCodeSetting.Value.ProductUrlFormat_Hotel;

                        url = url.Replace("{StartDate}", DateTime.Now.ToString("yyyy-MM-dd"))
                                 .Replace("{EndDate}", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"))
                                 .Replace("{HotelId}", p.OutterId);

                        break;
                    case ProductTypeEnum.度假:
                        url = _oneCodeSetting.Value.ProductUrlFormat_Vacation;

                        url = url.Replace("{VacationId}", p.OutterId);

                        break;
                }
                p.Url = url;
            });

            return ResponseReturn.ReturnSuccess(
                data: h5ShopDto
                );
        }

        /// <summary>
        /// 查询店铺佣金信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseReturn> GetCommisionInfoAsync(Guid id, GetCommisionInfoInputDto input)
        {
            var shop = (await _shopRepository.FindAsync(id, false)).CheckNullOrDeleted();


            return ResponseReturn.ReturnSuccess(
                data: new GetShopCommisionInfoDto
                {
                    AccumulatedCommision = await _orderRepository.GetAccumulatedCommisionAsync(id, input.SalerId, input.SalerId == shop.OwnerId),
                    TodayCommision = await _orderRepository.GetTodayCommisionAsync(id, input.SalerId),
                    DrewCommision = (input.SalerId.HasValue && input.SalerId != shop.OwnerId) ? 0.00M : await _drawRepository.GetDrewCommisionAsync(id),
                    AvailableCommision = (input.SalerId.HasValue && input.SalerId != shop.OwnerId) ? 0.00M : shop.CommisionAvailable
                });
        }

        /// <summary>
        /// 查询店铺佣金明细
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<ResponseReturn> GetCommisionDetailsAsync(Guid id, GetShopCommisionDetailsInputDto input)
        {
            return ResponseReturn.ReturnSuccess(
                data: ObjectMapper.Map<List<Order>, List<GetShopCommisionDetailsDto>>(await _orderRepository.GetListAsync(
                    filter: null,
                    outterOrderId: null,
                    salerId: input.SalerId,
                    shopId: id,
                    startDate: input.Date ?? DateTime.Now.Date,
                    endDate: input.Date ?? DateTime.Now.Date,
                    orderBizStatus: null,
                    orderStatus: null
                    ))
               );
        }

        private string GeneratorQRCode(string text, int pixels = 4)
        {
            //var qrtext = _oneCodeSetting.Value.SalerQRCodeFormat.Replace("{shopId}", shopId.ToString()).Replace("{salerId}", salerId.ToString());

            var date = DateTime.Now.ToString("yyyyMMdd");

            //文件夹路径
            var fileFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "upload", date);

            //创建文件夹
            if (!Directory.Exists(fileFolder))
            {
                Directory.CreateDirectory(fileFolder);
            }

            //文件名
            var fileName = GuidGenerator.Create().ToString() + ".jpg";

            //文件完整路径
            var filePath = Path.Combine(fileFolder, fileName);

            //保存图片
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                //生成二维码流
                var image = QRCodeHelper.GetQRCode(text, pixels, System.Drawing.Color.Black, System.Drawing.Color.White, QRCodeGenerator.ECCLevel.M);

                //保存
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            return _oneCodeSetting.Value.UploadUrl + "/" + date + "/" + fileName;
        }
    }

}
