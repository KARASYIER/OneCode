using OneCode.EnumTypes;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace OneCode.Domain
{
    /// <summary>
    /// 佣金流水记录
    /// </summary>
    public class CommisionRecord : FullAuditedAggregateRoot<Guid>
    {
        #region Property
        /// <summary>
        /// 关联的Id
        /// </summary>
        public Guid RelationId { get; set; }

        /// <summary>
        /// 流水标识(订单/提现)
        /// </summary>
        public RecordFlag RecordFlag { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary>
        public Guid ShopId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 入账=分销员Id
        /// 提现=分销员(管理员)Id
        /// </summary>
        public Guid? SalerId { get; set; }

        /// <summary>
        /// 店铺负责人姓名
        /// </summary>
        public string SalerName { get; set; }

        /// <summary>
        /// 本次操作的佣金金额(包含负数)
        /// </summary>
        public decimal CommisionAmount { get; set; }

        /// <summary>
        /// 当前剩余可提现的佣金金额
        /// </summary>
        public decimal CommisionAvailable { get; set; }
        #endregion

        #region Ctor
        public CommisionRecord()
        {

        }

        public CommisionRecord(Guid id) : base(id)
        {
        }

        #endregion
    }
}
