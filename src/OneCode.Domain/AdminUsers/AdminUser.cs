using OneCode.EnumTypes;
using System;
using Volo.Abp.Domain.Entities.Auditing;


namespace OneCode.Domain
{
    /// <summary>
    /// 后台管理员
    /// </summary>
    public class AdminUser : FullAuditedAggregateRoot<Guid>
    {
        #region Ctor
        public AdminUser()
        {

        }
        #endregion

        #region Property

        /// <summary>
        /// 登录账号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 账号状态
        /// </summary>
        public AdminUserStatusEnum Status { get; set; }

        #endregion
    }
}
