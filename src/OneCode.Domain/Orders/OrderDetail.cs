using OneCode.EnumTypes;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace OneCode.Domain
{
    public class OrderDetail : FullAuditedAggregateRoot<Guid>
    {
        #region Property

        /// <summary>
        /// 分销订单关联号
        /// </summary>
        public virtual Guid OrderId { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        public virtual Guid ProductId { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public virtual string ProductTypeName { get; set; }

        /// <summary>
        /// 产品分类（车票、酒店、车酒、度假）
        /// </summary>
        public virtual ProductTypeEnum ProductType { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public virtual string ProductTitle { get; set; }

        /// <summary>
        /// 外部产品Id
        /// </summary>
        public virtual string OutterProductId { get; set; }

        /// <summary>
        /// 产品销售金额
        /// </summary>
        public virtual decimal Amount { get; set; }

        /// <summary>
        /// 订单(退款)差额
        /// </summary>
        public virtual decimal RefundBalance { get; set; } = 0;

        /// <summary>
        /// 当前剩余总金额
        /// </summary>
        public virtual decimal RemainAmount { get; set; }

        /// <summary>
        /// 剩余结算金额
        /// </summary>
        public virtual decimal RemainSettlementAmount { get; set; }

        /// <summary>
        /// 产品利润
        /// </summary>
        public virtual decimal Profit { get; set; }

        /// <summary>
        /// 数量(酒店为 一间夜)
        /// </summary>
        public virtual int Count { get; set; }

        /// <summary>
        /// 佣金类型
        /// </summary>
        public virtual CommisionTypeEnum CommisionType { get; set; }

        /// <summary>
        /// 产品佣金率
        /// </summary>
        public virtual decimal CommisionRate { get; set; }

        /// <summary>
        /// 佣金金额
        /// </summary>
        public virtual decimal CommisionValue { get; set; }

        /// <summary>
        /// 产品佣金[Rate]=产品利润*产品佣金率
        /// 产品佣金[Value]=佣金金额
        /// </summary>
        public virtual decimal Commision { get; set; }

        #endregion

        #region Ctor
        public OrderDetail()
        {
        }

        public OrderDetail(Guid id) : base(id)
        {
        }
        public OrderDetail(Guid id, Guid orderId) : base(id)
        {
            this.OrderId = orderId;
        }



        #endregion

    }
}
