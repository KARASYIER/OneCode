using System;
using System.Collections.Generic;
using System.Text;

namespace OneCode
{
    public class OneCodeExceptionCode
    {
        private const string ErrorCode_1000 = "该订单号重复提交";
        private const string ErrorCode_1001 = "";
        private const string ErrorCode_1002 = "";
        private const string ErrorCode_1003 = "";
        private const string ErrorCode_1004 = "";
        private const string ErrorCode_1005 = "";
        private const string ErrorCode_1006 = "";
        private const string ErrorCode_1007 = "";
        private const string ErrorCode_1008 = "";
        private const string ErrorCode_1009 = "";
        private const string ErrorCode_1010 = "";

        public string GetErrorMessage(int code)
        {
            var msg = string.Empty;

            switch (code)
            {
                case 1000: msg = ErrorCode_1000; break;
            }

            return msg;
        }
    }
}
