using OneCode.Dtos;
using OneCode.EnumTypes;
using OneCode.Shops.Dtos;
using OneCode.ToolKit.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneCode.Application.Contracts
{
    /// <summary>
    /// 店铺操作接口
    /// </summary>
    public interface IShopAppService
    {
        /// <summary>
        /// 创建的店铺
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> CreateAsync(CreateOrUpdateShopInputDto input);

        /// <summary>
        /// 删除店铺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseReturn> DeleteAsync(Guid id);

        /// <summary>
        /// 修改店铺
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateAsync(Guid id, CreateOrUpdateShopInputDto input);

        /// <summary>
        /// 修改店铺名称
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shopName"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateShopNameAsync(Guid id, string shopName);

        /// <summary>
        /// 修改店铺状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shopStatus"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateStatusAsync(Guid id, ShopStatusEnum shopStatus);

        /// <summary>
        /// 更新店铺的管理员
        /// </summary>
        /// <param name="id"></param>
        /// <param name="salerId"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateOwnerAsync(Guid id, Guid salerId);

        /// <summary>
        /// 修改佣金比例
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateCommisionRateAsync(Guid id, UpdateCommisionRateInputDto input);

        /// <summary>
        /// 设置关联
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateRelatedProductsAsync(Guid id, List<UpdateRelatedProductsInputDto> inputs);

        /// <summary>
        /// 取消关联
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productIds"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateUnrelatedProductsAsync(Guid id, List<UpdateRelatedProductsInputDto> inputs);

        /// <summary>
        /// 修改排序序号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateDisplayOrderAsync(Guid id, UpdateDisplayOrderInputDto input);

        /// <summary>
        /// 查询店铺信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetAsync(Guid id);

        /// <summary>
        /// 条件查询店铺
        /// </summary>
        /// <returns></returns>
        Task<ResponseReturn> GetListAsync(GetShopsInputDto input);

        /// <summary>
        /// 根据标签查询店铺
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetListByTagIdAsync(Guid tagId);

        /// <summary>
        /// 获取店铺下的分销员
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetSalersAsync(Guid id, PagedInputDto input);

        /// <summary>
        /// 获取店铺状态类型
        /// </summary>
        /// <returns></returns>
        Task<ResponseReturn> GetStatusOptionsAsync();

        /// <summary>
        /// 获取已关联产品
        /// </summary>
        /// <returns></returns>
        Task<ResponseReturn> GetRelatedProductsAsync(Guid id, PagedInputDto input);

        /// <summary>
        /// 获取未关联产品
        /// </summary>
        /// <returns></returns>
        Task<ResponseReturn> GetUnrelatedProductsAsync(Guid id, PagedInputDto input);

        Task<ResponseReturn> GetFullAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetShopWithProductsAsync(Guid id);

        /// <summary>
        /// 获取店铺佣金信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetCommisionInfoAsync(Guid id, GetCommisionInfoInputDto input);

        /// <summary>
        /// 查询店铺佣金明细
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetCommisionDetailsAsync(Guid id, GetShopCommisionDetailsInputDto input);
    }
}
