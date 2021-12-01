using AutoMapper;
using OneCode.BizImages.Dtos;
using OneCode.Domain;
using OneCode.Draws.Dtos;
using OneCode.Orders.Dtos;
using OneCode.Products.Dtos;
using OneCode.Salers.Dtos;
using OneCode.Shops.Dtos;
using OneCode.Tags.Dtos;
using System.Collections.Generic;
using Volo.Abp.AutoMapper;

namespace OneCode.Application
{
    public class OneCodeApplicationAutoMapperProfile : Profile
    {
        public OneCodeApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<Product, ProductDto>();

            CreateMap<Product, ProductAllShopDto>().ForMember(p => p.Shops, p => p.MapFrom(x => x.ShopProducts));
            CreateMap<CreateProductInputDto, Product>();
            CreateMap<CreateOrUpdateProductInputBaseDto, Product>();
            CreateMap<UpdateProductInputDto, Product>();

            CreateMap<Shop, ShopDto>();
            CreateMap<Shop, ShopFullDto>().ForMember(p => p.Tags, p => p.MapFrom(x => x.ShopTags));

            CreateMap<Shop, ShopAllProductDto>().ForMember(p => p.Products, p => p.MapFrom(x => x.ShopProducts));
            CreateMap<CreateOrUpdateShopInputDto, Shop>();

            CreateMap<Saler, SalerDto>();
            CreateMap<CreateOrUpdateSalerInputBaseDto, Saler>();
            CreateMap<CreateOrUpdateSalerInputDto, Saler>();

            CreateMap<Saler, GetSalersResultDto>().ForMember(p => p.ShopName, p => p.MapFrom(x => x.Shop.Name));

            CreateMap<ShopProduct, Products.Dtos.ShopProductDto>();
            CreateMap<ShopProduct, ShopDto>().IncludeMembers(p => p.Shop).ForMember(p => p.Id, p => p.MapFrom(x => x.ShopId));

            CreateMap<Product, ShopRelatedProductDto>();
            CreateMap<Product, ShopUnrelatedProductDto>();

            CreateMap<ShopProduct, ShopRelatedProductDto>().IncludeMembers(p => p.Product)
                                                           .ForMember(p => p.Id, p => p.MapFrom(x => x.ProductId))
                                                           .ForMember(p => p.OriginCommisionRate, p => p.MapFrom(x => x.Product.CommisionRate))
                                                           .ForMember(p => p.CommisionRate, p => p.MapFrom(x => x.CommisionRate));


            CreateMap<Shop, ProductRelatedShopDto>();
            CreateMap<Product, ProductRelatedShopDto>();
            CreateMap<Shop, ProductUnrelatedShopDto>();

            CreateMap<ShopProduct, ProductRelatedShopDto>().IncludeMembers(p => p.Shop)
                                                           .IncludeMembers(p => p.Product)
                                                           .ForMember(p => p.Id, p => p.MapFrom(x => x.ShopId))
                                                           .ForMember(p => p.Name, p => p.MapFrom(x => x.Shop.Name))
                                                           .ForMember(p => p.OriginCommisionRate, p => p.MapFrom(x => x.Product.CommisionRate))
                                                           .ForMember(p => p.OriginCommisionValue, p => p.MapFrom(x => x.Product.CommisionValue))
                                                           .ForMember(p => p.CommisionType, p => p.MapFrom(x => x.CommisionType))
                                                           .ForMember(p => p.CommisionRate, p => p.MapFrom(x => x.CommisionRate))
                                                           .ForMember(p => p.CommisionValue, p => p.MapFrom(x => x.CommisionValue));

            CreateMap<ShopProduct, Shops.Dtos.ShopProductDto>();
            CreateMap<Shop, ShopFullTestDto>();


            CreateMap<Tag, TagDto>();
            CreateMap<ShopTag, TagDto>().IncludeMembers(p => p.Tag);

            CreateMap<CreateOrUpdateTagInputDto, Tag>();

            CreateMap<BizImage, BizImageDto>();

            CreateMap<CreateOrderDetailInputDto, OrderDetail>();
            CreateMap<RefundPartialOrderDetailInputDto, OrderDetail>();
            CreateMap<CreateOrderInputDto, Order>();

            CreateMap<Order, OrderDto>();
            CreateMap<Order, CreateOrderResultDto>();
            CreateMap<Order, OrderFullDto>().ForMember(p => p.Details, p => p.MapFrom(x => x.OrderDetails));
            CreateMap<OrderDetail, OrderDetailDto>();

            CreateMap<Draw, DrawDto>();

            CreateMap<Order, GetShopCommisionDetailsDto>();

            #region H5

            CreateMap<Product, H5ProductDto>();
            CreateMap<ShopProduct, H5ProductDto>().IncludeMembers(p => p.Product);
            CreateMap<Shop, H5ShopWithProductsDto>();//.IncludeMembers(p => p.ShopProducts).ForMember(p => p.Products, p => p.MapFrom(x => x.ShopTags));

            #endregion
        }
    }
}
