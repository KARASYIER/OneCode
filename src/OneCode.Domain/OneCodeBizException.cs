using System;

namespace OneCode
{
    public class OneCodeBizException : Exception
    {
        private int _errorCode = -1;


        public OneCodeBizException(string message) : base(message)
        {

        }

        public OneCodeBizException(int errorCode, string message) : base(message)
        {
            _errorCode = errorCode;
            
        }

        public int ErrorCode
        {
            get
            {
                return _errorCode;
            }
        }
    }
}
