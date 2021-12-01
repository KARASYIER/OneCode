using System.ComponentModel;

namespace OneCode.EnumTypes
{
    public enum ShopStatusEnum
    {
        /// <summary>
        /// 营业
        /// </summary>
        [Description("营业")]
        Openning = 0,


        /// <summary>
        /// 打烊
        /// </summary>
        [Description("打烊")]
        Paused = 1,

        /// <summary>
        /// 停业
        /// </summary>
        [Description("停业")]
        Closed = 2
    }
}
