using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace OneCode.ToolKit.Http
{
    public static class HttpTools
    {
        /// <summary>
        /// 请求远程Api获取响应返回字符串
        /// </summary>
        /// <param name="apiUrl">Api地址</param>
        /// <param name="parameters">传递参数键值对</param>
        /// <param name="contentType">内容类型默认application/x-www-form-urlencoded</param>
        /// <param name="methord">请求方式默认POST</param>
        /// <param name="timeout">超时时间默认300000</param>
        /// <returns>响应字符串</returns>
        public static string GetHttpWebResponseReturnString(string apiUrl, Dictionary<string, string> parameters = null, string postData = "", string contentType = "application/x-www-form-urlencoded", string methord = "POST", int timeout = 300000)
        {
            string result = string.Empty;
            string responseText = string.Empty;

            if (string.IsNullOrEmpty(apiUrl))
            {
                throw new ArgumentNullException("apiUrl");
            }

            StringBuilder querystring = new StringBuilder();
            if (parameters != null && parameters.Count > 0)
            {
                foreach (var p in parameters)
                {
                    if (postData.Length == 0)
                    {
                        querystring.AppendFormat("?{0}={1}", p.Key, p.Value);
                    }
                    else
                    {
                        querystring.AppendFormat("&{0}={1}", p.Key, p.Value);
                    }
                }
            }

            ServicePointManager.DefaultConnectionLimit = int.MaxValue;

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(apiUrl + querystring);
            myRequest.Proxy = null;
            myRequest.Timeout = timeout;
            myRequest.ServicePoint.MaxIdleTime = 1000;
            if (!string.IsNullOrEmpty(contentType))
            {
                myRequest.ContentType = contentType;
            }
            myRequest.ServicePoint.Expect100Continue = false;
            myRequest.Method = methord;
            byte[] postByte = Encoding.UTF8.GetBytes(postData.ToString());
            myRequest.ContentLength = postData.Length;

            using (Stream writer = myRequest.GetRequestStream())
            {
                writer.Write(postByte, 0, postData.Length);
            }

            using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
            {
                using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
                {
                    responseText = reader.ReadToEnd();
                }
            }
            if (!string.IsNullOrEmpty(responseText))
            {
                result = responseText;
            }
            else
            {
                result = "远程服务无响应，请稍后再试";
            }
            return result;
        }
    }
}
