using OneCode.EnumTypes;
using System;
using System.Collections;
using Volo.Abp.Domain.Entities.Auditing;

namespace OneCode.Domain
{
    /// <summary>
    /// 分销员
    /// </summary>
    public class Saler : FullAuditedAggregateRoot<Guid>
    {
        #region Property

        /// <summary>
        /// 手机号/登录账号
        /// </summary>
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// 分销员姓名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 分销员类型(店铺管理员、普通分销员等）
        /// </summary>
        public virtual SalerTypeEnum Type { get; set; }

        /// <summary>
        /// 微信openid
        /// </summary>
        public virtual string Openid { get; set; }

        /// <summary>
        /// 微信头像地址
        /// </summary>
        public virtual string HeadImgUrl { get; set; }

        /// <summary>
        /// 微信昵称
        /// </summary>
        public virtual string Nickname { get; set; }

        /// <summary>
        /// 推广的二维码地址
        /// </summary>
        public virtual string QRCodeUrl { get; set; }

        /// <summary>
        /// 当前分销员状态
        /// 启用/禁用
        /// </summary>
        public virtual bool Status { get; set; }

        /// <summary>
        /// 用户所属店铺
        /// </summary>
        public virtual Guid ShopId { get; set; }
        #endregion

        public virtual Shop Shop { get; set; }

        #region Ctor
        public Saler()
        {

        }

        public Saler(Guid id): base(id)
        {

        }

        #endregion
    }
}
