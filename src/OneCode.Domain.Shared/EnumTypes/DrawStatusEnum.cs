using System.ComponentModel.DataAnnotations;

namespace OneCode.EnumTypes
{
    /// <summary>
    /// 提现审核状态枚举类型
    /// </summary>
    public enum DrawStatusEnum
    {
        /// <summary>
        /// 审核中
        /// </summary>
        [Display(Name = "审核中")]
        Approving = 0,
        /// <summary>
        /// 已审核
        /// </summary>
        [Display(Name = "已审核")]
        Approved = 1
    }
}
