using OneCode.Dtos;
using OneCode.EnumTypes;
using OneCode.Salers.Dtos;
using OneCode.ToolKit.Http;
using System;
using System.Threading.Tasks;

namespace OneCode.Application.Contracts
{
    /// <summary>
    /// 分销员(含管理员)操作接口
    /// </summary>
    public interface ISalerAppService
    {
        /// <summary>
        /// 创建分销员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> CreateAsync(CreateOrUpdateSalerInputDto input);


        /// <summary>
        /// 分销员账户登录
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ResponseReturn> LoginAsync(string mobile, string password);

        /// <summary>
        /// 删除分销员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseReturn> DeleteAsync(Guid id);

        /// <summary>
        /// 修改分销员信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateAsync(Guid id, CreateOrUpdateSalerInputBaseDto input);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdatePasswordAsync(Guid id, UpdateSalerPasswordInputDto input);

        /// <summary>
        /// 修改分销员状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<ResponseReturn> UpdateStatusAsync(Guid id);

        /// <summary>
        /// 查询分销员信息
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetAsync(Guid id);

        /// <summary>
        /// 查询分销员列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseReturn> GetListAsync(GetSalersInputDto input);

        /// <summary>
        /// 获取分销员类型选项(分销员/店铺负责人)
        /// </summary>
        /// <returns></returns>
        Task<ResponseReturn> GetTypeAsync();
    }
}
