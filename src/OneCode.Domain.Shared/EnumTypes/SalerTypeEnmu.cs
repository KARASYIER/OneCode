using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OneCode.EnumTypes
{
    public enum SalerTypeEnum
    {
        /// <summary>
        /// 店负责人
        /// </summary>
        [Description("店铺负责人")]
        Owner = 0,
        /// <summary>
        /// 销售员/分销员
        /// </summary>
        [Description("分销员")]
        Saler = 1
    }
}
