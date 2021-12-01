using OneCode.Dtos;
using OneCode.EnumTypes;
using OneCode.Products.Dtos;
using OneCode.ToolKit.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneCode.Application.Contracts
{
    public interface IProductAppService
    {
        /// <summary>
        /// 创建产品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> CreateAsync(CreateProductInputDto input);

        /// <summary>
        /// 更新产品信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateAsync(Guid id, UpdateProductInputDto input);

        /// <summary>
        /// 删除一个产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseReturn> DeleteAsync(Guid id);

        /// <summary>
        /// 更新产品销售状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateSaleStatusAsync(Guid id);

        /// <summary>
        /// 更新产品库存状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateStockStatusAsync(Guid id);

        /// <summary>
        /// 修改产品佣金倍率
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productId"></param>
        /// <param name="CommisionRate"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateBasicCommisionAsync(Guid id, UpdateCommisionRateOrValueInputDto input);

        /// <summary>
        /// 设置排序
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateDisplayOrderAsync(UpdateProductDisplayOrderInputDto input);

        /// <summary>
        /// 查询产品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetAsync(Guid id);

        /// <summary>
        /// 分页查询产品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetListAsync(GetProductsInputDto input);

        /// <summary>
        /// 获取已关联的店铺
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetRelatedShopsAsync(Guid id, PagedInputDto input);

        /// <summary>
        /// 获取未关联的店铺
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetUnrelatedShopsAsync(Guid id, PagedInputDto input);

        /// <summary>
        /// 设置关联店铺
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateRelatedShopsAsync(Guid id, List<UpdateRelatedShopsInputDto> inputs);

        /// <summary>
        /// 取消关联店铺
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateUnrelatedShopsAsync(Guid id, List<Guid> shopIds);
    }
}
