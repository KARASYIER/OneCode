using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneCode.EnumTypes
{
    /// <summary>
    /// 分销员/销售员状态枚举类型
    /// </summary>
    public enum SalerStatusEnum
    {
        [Description("启用")]
        Enable = 0,

        [Description("禁用")]
        Disable = 1,

    }
}
