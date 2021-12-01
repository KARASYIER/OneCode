using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace OneCode.Domain
{
    /// <summary>
    /// 分销订单
    /// 内部订单号为主键Id
    /// </summary>
    public class Order : FullAuditedAggregateRoot<Guid>
    {
        #region Property

        /// <summary>
        /// 订单标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public virtual decimal TotalAmount { get; set; }

        /// <summary>
        /// 结算金额
        /// </summary>
        public virtual decimal SettlementAmount { get; set; }

        /// <summary>
        /// 利润(暂时弃用)
        /// </summary>
        public virtual decimal TotalPorfit { get; set; }

        /// <summary>
        /// 合计佣金
        /// </summary>
        public virtual decimal TotalCommision { get; set; }

        /// <summary>
        /// 分销店铺Id
        /// </summary>
        public virtual Guid ShopId { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public virtual string ShopName { get; set; }

        /// <summary>
        /// 销售员(分销员)Id
        /// </summary>
        public virtual Guid? SalerId { get; set; }

        /// <summary>
        /// 分销员名称
        /// </summary>
        public virtual string SalerName { get; set; }

        /// <summary>
        /// 订单状态（从时间角度）
        /// </summary>
        public virtual OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// 订单完成时间
        /// </summary>
        public virtual DateTime? FinishedDate { get; set; }

        /// <summary>
        /// 订单业务状态
        /// </summary>

        public virtual OrderBizStatus BizStatus { get; set; }

        /// <summary>
        /// 外部订单编号
        /// </summary>
        public virtual string OutterOrderId { get; set; }
        #endregion

        #region Navigation Property
        /// <summary>
        /// 订单明细
        /// </summary>
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        #endregion

        #region Ctor
        public Order()
        {

        }

        public Order(Guid id) : base(id)
        {
        }

        #endregion
    }
}
