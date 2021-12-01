using OneCode.AdminUser.Dtos;
using OneCode.Dtos;
using OneCode.EnumTypes;
using System;
using System.Threading.Tasks;

namespace OneCode.Application.Contracts
{
    /// <summary>
    /// 管理员操作接口
    /// </summary>
    public interface IAdminUserAppServic
    {
        /// <summary>
        /// [通用]获取管理员状态选项
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<OptionResultDto>> GetAdminUserStatusOptionsAsync();

        /// <summary>
        /// [管理员]登录
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ResultDto> LoginAsync(string mobile, string password);

        /// <summary>
        /// [管理员]查看当前登录账号信息
        /// </summary>
        /// <returns></returns>
        Task<SingleResultDto<AdminUserDto>> GetCurrentInfoAsync();

        /// <summary>
        /// [管理员]修改当前账号密码
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResultDto> UpdatePasswordAsync(Guid adminUserId, UpdateAdminUserPasswordInputDto input);


        /// <summary>
        /// [超级管理员]创建管理员账户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SingleResultDto<AdminUserDto>> CreateAdminUserAsync(CreateOrUpdateAdminUserInputDto input);

        /// <summary>
        /// [超级管理员]更新管理员账户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SingleResultDto<AdminUserDto>> UpdateAdminUserAsync(CreateOrUpdateAdminUserInputDto input);

        /// <summary>
        /// [超级管理员]更新管理员账户状态
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<ResultDto> UpdateAdminUserStatusAsync(Guid adminUserId, AdminUserStatusEnum status);

        /// <summary>
        /// [超级管理员]删除管理员账户
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <returns></returns>
        Task<ResultDto> DeleteAdminUserAsync(Guid adminUserId);

        /// <summary>
        /// [超级管理员]查询管理员账户信息
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <returns></returns>
        Task<SingleResultDto<AdminUserDto>> GetAdminUserAsync(Guid adminUserId);

        /// <summary>
        /// [超级管理员]分页查询管理员账户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<AdminUserDto>> GetAdminUsersAsync(GetAdminUsersInputDto input);

        /// <summary>
        /// [超级管理员]查询管理员账户权限(暂不使用)
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<object>> GetAdminUserRolesAsync(Guid adminUserId);

    }
}
