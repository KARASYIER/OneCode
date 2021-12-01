namespace OneCode
{
    public static class OneCodeDomainErrorCodes
    {
        public const string ErrMsg_1001 = "没有查询到相关产品数据";
        public const string ErrMsg_1002 = "产品名称不可为空";
        public const string ErrMsg_1003 = "错误的产品类型";
        public const string ErrMsg_1004 = "产品佣金率取值应在0.00~1.00之间";
        public const string ErrMsg_1005 = "产品固定佣金金额取值不可小于0或者大于100";

        public const string ErrMsg_2001 = "没有查询到相关店铺数据";
        public const string ErrMsg_2002 = "店铺名称不可为空";
        public const string ErrMsg_2003 = "设置负责人失败，该分销员不存在可能已删除，或者该分销员不属于该店铺";
        public const string ErrMsg_3001 = "该外部订单号不存在";
        public const string ErrMsg_3002 = "该订单号重复提交";
        public const string ErrMsg_3003 = "无法从明细列表中提供的产品编号和类型，检索到相关数据";
        public const string ErrMsg_3004 = "无法从明细列表中提供的产品编号和类型，检索到与创建时相匹配的数据";
        public const string ErrMsg_3005 = "订单金额验证失败";
        public const string ErrMsg_3006 = "没有要处理退款的订单明细";
        public const string ErrMsg_3007 = "该订单的状态已完成，无法进行退单处理";
        public const string ErrMsg_3008 = "没有查询到店铺信息";
        public const string ErrMsg_3009 = "没有查询到分销员信息";
        public const string ErrMsg_4001 = "没有查询到相关提现记录";
        public const string ErrMsg_4002 = "该笔提现已审核";
        public const string ErrMsg_4003 = "提现需要先绑定店铺负责人,由负责人申请提现";
        public const string ErrMsg_4004 = "当前申请人非该店铺负责人,无法申请提现";
        public const string ErrMsg_4005 = "当前可申请提现的金额与申请的金额不匹配";
        public const string ErrMsg_4006 = "当前有正在申请的提现,无法进行多次申请";
        public const string ErrMsg_4007 = "当前可提现的金额与提交的不符合,请联系相关技术人员";
        public const string ErrMsg_4008 = "店铺尚未绑定负责人,无法创建提现记录";
        public const string ErrMsg_8001 = "数据提交处理失败";
        public const string ErrMsg_9001 = "提交的数据对象格式错误：可能Json格式不正确或者类型不匹配";
        public const string ErrMsg_9002 = "计算佣金时使用了未知的佣金类型";
        public const string ErrMsg_9003 = "错误的产品类型（1=车票，2=酒店，6=度假）";
        public const string ErrMsg_9999 = "未知错误";

        public static string GetErrorMessage(int errorCode)
        {
            switch (errorCode)
            {
                case 1005: return ErrMsg_1005;
                case 1001: return ErrMsg_1001;
                case 1002: return ErrMsg_1002;
                case 1003: return ErrMsg_1003;
                case 1004: return ErrMsg_1004;
                case 2001: return ErrMsg_2001;
                case 2002: return ErrMsg_2002;
                case 2003: return ErrMsg_2003;
                case 3001: return ErrMsg_3001;
                case 3002: return ErrMsg_3002;
                case 3003: return ErrMsg_3003;
                case 3004: return ErrMsg_3004;
                case 3005: return ErrMsg_3005;
                case 3006: return ErrMsg_3006;
                case 3007: return ErrMsg_3007;
                case 3008: return ErrMsg_3008;
                case 3009: return ErrMsg_3009;
                case 4001: return ErrMsg_4001;
                case 4002: return ErrMsg_4002;
                case 4003: return ErrMsg_4003;
                case 4004: return ErrMsg_4004;
                case 4005: return ErrMsg_4005;
                case 4006: return ErrMsg_4006;
                case 4007: return ErrMsg_4007;
                case 4008: return ErrMsg_4008;
                case 8001: return ErrMsg_8001;
                case 9001: return ErrMsg_9001;
                case 9002: return ErrMsg_9002;
                default: return ErrMsg_9999;
            }
        }

        public static int GetErrorCode(string errorMessage)
        {
            switch (errorMessage)
            {
                case ErrMsg_1001: return 1001;
                case ErrMsg_1002: return 1002;
                case ErrMsg_1003: return 1003;
                case ErrMsg_1004: return 1004;
                case ErrMsg_1005: return 1005;
                case ErrMsg_2001: return 2001;
                case ErrMsg_2002: return 2002;
                case ErrMsg_2003: return 2003;
                case ErrMsg_3001: return 3001;
                case ErrMsg_3002: return 3002;
                case ErrMsg_3003: return 3003;
                case ErrMsg_3004: return 3004;
                case ErrMsg_3005: return 3005;
                case ErrMsg_3006: return 3006;
                case ErrMsg_3007: return 3007;
                case ErrMsg_3008: return 3008;
                case ErrMsg_3009: return 3009;
                case ErrMsg_4001: return 4001;
                case ErrMsg_4002: return 4002;
                case ErrMsg_4003: return 4003;
                case ErrMsg_4004: return 4004;
                case ErrMsg_4005: return 4005;
                case ErrMsg_4006: return 4006;
                case ErrMsg_4007: return 4007;
                case ErrMsg_4008: return 4008;
                case ErrMsg_8001: return 8001;
                case ErrMsg_9001: return 9001;
                case ErrMsg_9002: return 9002;
                case ErrMsg_9003: return 9003;
                default: return 9999;
            }
        }

        public static string GetErrorMessage(OneCodeErrorCodes errorCode)
        {
            switch (errorCode)
            {
                case OneCodeErrorCodes.ErrMsg_1001: return "没有查询到相关产品数据";
                case OneCodeErrorCodes.ErrMsg_1002: return "产品名称不可为空";
                case OneCodeErrorCodes.ErrMsg_1003: return "错误的产品类型";
                case OneCodeErrorCodes.ErrMsg_1004: return "产品佣金率取值应在0.00~1.00之间";
                case OneCodeErrorCodes.ErrMsg_1005: return "产品固定佣金金额取值不可小于0或者大于100";
                case OneCodeErrorCodes.ErrMsg_2001: return "没有查询到相关店铺数据";
                case OneCodeErrorCodes.ErrMsg_2002: return "店铺名称不可为空";
                case OneCodeErrorCodes.ErrMsg_2003: return "设置负责人失败，该分销员不存在可能已删除，或者该分销员不属于该店铺";
                case OneCodeErrorCodes.ErrMsg_3001: return "该外部订单号不存在";
                case OneCodeErrorCodes.ErrMsg_3002: return "该订单号重复提交";
                case OneCodeErrorCodes.ErrMsg_3003: return "无法从明细列表中提供的产品编号和类型，检索到相关数据";
                case OneCodeErrorCodes.ErrMsg_3004: return "无法从明细列表中提供的产品编号和类型，检索到与创建时相匹配的数据";
                case OneCodeErrorCodes.ErrMsg_3005: return "订单金额验证失败";
                case OneCodeErrorCodes.ErrMsg_3006: return "没有要处理退款的订单明细";
                case OneCodeErrorCodes.ErrMsg_3007: return "该订单的状态已完成，无法进行退单处理";
                case OneCodeErrorCodes.ErrMsg_3008: return "没有查询到店铺信息";
                case OneCodeErrorCodes.ErrMsg_3009: return "没有查询到分销员信息";
                case OneCodeErrorCodes.ErrMsg_4001: return "没有查询到相关提现记录";
                case OneCodeErrorCodes.ErrMsg_4002: return "该笔提现已审核";
                case OneCodeErrorCodes.ErrMsg_4003: return "提现需要先绑定店铺负责人,由负责人申请提现";
                case OneCodeErrorCodes.ErrMsg_4004: return "当前申请人非该店铺负责人,无法申请提现";
                case OneCodeErrorCodes.ErrMsg_4005: return "当前可申请提现的金额与申请的金额不匹配";
                case OneCodeErrorCodes.ErrMsg_4006: return "当前有正在申请的提现,无法进行多次申请";
                case OneCodeErrorCodes.ErrMsg_4007: return "当前可提现的金额与提交的不符合,请联系相关技术人员";
                case OneCodeErrorCodes.ErrMsg_4008: return "店铺尚未绑定负责人,无法创建提现记录";
                case OneCodeErrorCodes.ErrMsg_8001: return "数据提交处理失败";
                case OneCodeErrorCodes.ErrMsg_9001: return "提交的数据对象格式错误：可能Json格式不正确或者类型不匹配";
                case OneCodeErrorCodes.ErrMsg_9002: return "计算佣金时使用了未知的佣金类型";
                case OneCodeErrorCodes.ErrMsg_9003: return "使用了不";
                case OneCodeErrorCodes.ErrMsg_9999: return "未知错误";
                default: return "未知错误";
            }
        }
        /* You can add your business exception error codes here, as constants */
    }

    public enum OneCodeErrorCodes
    {
        ErrMsg_1005 = 1005,
        ErrMsg_1001 = 1001,
        ErrMsg_1002 = 1002,
        ErrMsg_1003 = 1003,
        ErrMsg_1004 = 1004,
        ErrMsg_2001 = 2001,
        ErrMsg_2002 = 2002,
        ErrMsg_2003 = 2003,
        ErrMsg_3001 = 3001,
        ErrMsg_3002 = 3002,
        ErrMsg_3003 = 3003,
        ErrMsg_3004 = 3004,
        ErrMsg_3005 = 3005,
        ErrMsg_3006 = 3006,
        ErrMsg_3007 = 3007,
        ErrMsg_3008 = 3008,
        ErrMsg_3009 = 3009,
        ErrMsg_4001 = 4001,
        ErrMsg_4002 = 4002,
        ErrMsg_4003 = 4003,
        ErrMsg_4004 = 4004,
        ErrMsg_4005 = 4005,
        ErrMsg_4006 = 4006,
        ErrMsg_4007 = 4007,
        ErrMsg_4008 = 4008,
        ErrMsg_8001 = 8001,
        ErrMsg_9001 = 9001,
        ErrMsg_9002 = 9002,
        ErrMsg_9003 = 9003,
        ErrMsg_9999 = 9999
    }
}
