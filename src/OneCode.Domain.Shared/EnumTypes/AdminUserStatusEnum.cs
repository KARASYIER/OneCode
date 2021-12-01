using System.ComponentModel.DataAnnotations;

namespace OneCode.EnumTypes
{
    /// <summary>
    /// 管理员账号状态
    /// </summary>
    public enum AdminUserStatusEnum
    {
        [Display(Name = "启用" )]
        Enable = 0,
        
        [Display(Name = "禁用")]
        Disable = 1
    }
}
