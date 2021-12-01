using OneCode.EnumTypes;
using System;

namespace OneCode.Salers.Dtos
{
    /// <summary>
    /// 分销员
    /// </summary>
    public class SalerDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 手机号/登录账号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 分销员姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分销员类型(店铺管理员、普通分销员等）
        /// </summary>
        public SalerTypeEnum Type { get; set; }

        /// <summary>
        /// 微信openid
        /// </summary>
        public string Openid { get; set; }

        /// <summary>
        /// 微信头像地址
        /// </summary>
        public string HeadImgUrl { get; set; }

        /// <summary>
        /// 微信昵称
        /// </summary>
        public string Nickname { get; set; }

        public string QRCodeUrl { get; set; }

        /// <summary>
        /// 当前分销员状态
        /// 启用/禁用
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 用户所属店铺
        /// </summary>
        public Guid ShopId { get; set; }
    }
}
