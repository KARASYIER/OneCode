using OneCode.EnumTypes;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace OneCode.Domain
{
    /// <summary>
    /// 提现
    /// </summary>
    public class Draw : FullAuditedAggregateRoot<Guid>
    {
        #region Property 

        /// <summary>
        /// 负责人Id
        /// </summary>
        public virtual Guid OwnerId { get; set; }

        /// <summary>
        /// 提现操作的用户名称（即当前登录用户真实姓名）
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 提现操作人的手机号码（即当前登录手机号码）
        /// </summary>
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary>
        public virtual Guid ShopId { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public virtual string ShopName { get; set; }

        /// <summary>
        /// 申请提现金额
        /// </summary>
        public virtual decimal Amount { get; set; }

        /// <summary>
        /// 剩余可提的佣金
        /// </summary>
        public virtual decimal RemainCommision { get; set; }

        /// <summary>
        /// 提现状态
        /// </summary>
        public virtual DrawStatusEnum DrawStatus { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public virtual DateTime? ConfirmTime { get; set; }

        #endregion   

        #region ctor
        public Draw()
        {
        }

        public Draw(Guid id) : base(id)
        {
        }
        #endregion
    }
}
