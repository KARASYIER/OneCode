using OneCode.Dtos;
using OneCode.EnumTypes;

namespace OneCode.AdminUser.Dtos
{
    /// <summary>
    /// 查询后台管理员列表请求参数
    /// </summary>
    public class GetAdminUsersInputDto : PagedInputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AdminUserStatusEnum AdminUserStatus { get; set; }
    }
}
