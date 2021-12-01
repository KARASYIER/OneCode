namespace OneCode.Shops.Dtos
{
    public class GetShopCommisionInfoDto
    {
        /// <summary>
        /// 累计佣金
        /// </summary>
        public decimal AccumulatedCommision { get; set; }

        /// <summary>
        /// 今日佣金
        /// </summary>
        public decimal TodayCommision { get; set; }

        /// <summary>
        /// 已提佣金
        /// </summary>
        public decimal DrewCommision { get; set; }

        /// <summary>
        /// 可提佣金
        /// </summary>
        public decimal AvailableCommision { get; set; }
    }
}
