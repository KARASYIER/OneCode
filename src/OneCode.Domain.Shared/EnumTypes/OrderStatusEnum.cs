using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneCode.EnumTypes
{
    /// <summary>
    /// 订单进行状态枚举类型
    /// </summary>
    public enum OrderStatus
    {

        /// <summary>
        /// 订单进行中
        /// </summary>
        [Display(Name = "订单进行中")]
        Doing = 0,

        /// <summary>
        /// 订单已完成
        /// </summary>
        [Display(Name = "订单已完成")]
        Finished = 1
    }

    /// <summary>
    /// 订单业务状态枚举类型
    /// </summary>
    public enum OrderBizStatus
    {

        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 订单部分退
        /// </summary>
        [Display(Name = "订单部分退")]
        PartialRefund = 1,

        /// <summary>
        /// 订单整单退
        /// </summary>
        [Display(Name = "订单整单退")]
        FullRefund = 2,
    }
}
