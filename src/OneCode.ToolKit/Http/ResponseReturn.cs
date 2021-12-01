using System;
using System.Threading.Tasks;

namespace OneCode.ToolKit.Http
{
    [Serializable]
    public class ResponseReturn
    {
        #region Ctor
        private ResponseReturn()
        {

        }
        #endregion

        #region Property
        public int errcode { get; set; } = 0;

        public string errmsg { get; set; } = "";

        public object data { get; set; }
        #endregion


        public static ResponseReturn ReturnSuccess(string errmsg = "", object data = null)
        {
            var ret = new ResponseReturn()
            {
                errmsg = errmsg,
                data = data
            };
            return ret;
        }

        public static async Task<ResponseReturn> ReturnSuccessAsync(string errmsg = "", object data = null)
        {
            return await Task.Run(() => ReturnSuccess(errmsg, data));
        }

        public static ResponseReturn ReturnFailure(int errcode = -1, string errmsg = "", object data = null)
        {
            var ret = new ResponseReturn()
            {
                errcode = errcode,
                errmsg = errmsg,
                data = data
            };
            return ret;
        }

        public static async Task<ResponseReturn> ReturnFailureAsync(int errcode = -1, string errmsg = "", object data = null)
        {
            return await Task.Run(() => ReturnFailure(errcode, errmsg, data));
        }
    }
}
