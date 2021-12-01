using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OneCode.Middleware
{
    public class LoggingMiddleware
    {
        #region Fields
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private static Regex reUnicode = new Regex(@"\\u([0-9a-fA-F]{4})", RegexOptions.Compiled);
        private readonly IDictionary<string, LoggingScope> _routes;
        #endregion


        #region Ctor
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            this._logger = logger;
            this._next = next;
            
            this._recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }
        #endregion


        public async Task Invoke(HttpContext context)
        {
            LoggingScope loggingScope = GetLoggingScope(context);

            StringBuilder sbInfor = new StringBuilder();
            sbInfor.AppendLine();
            if (loggingScope == LoggingScope.Both || loggingScope == LoggingScope.RequestOnly)
            {
                sbInfor.AppendLine("Request:");
                sbInfor.AppendLine(await GetRequestContent(context.Request));
            }

            if (loggingScope == LoggingScope.RequestOnly)
            {
                _logger.LogInformation(sbInfor.ToString());
                await _next(context);
                return;
            }

            if (loggingScope == LoggingScope.Both)
            {
                var originalBody = context.Response.Body;
                using (MemoryStream newResponseBody = _recyclableMemoryStreamManager.GetStream())
                {
                    context.Response.Body = newResponseBody;
                    await _next(context);
                    newResponseBody.Seek(0, SeekOrigin.Begin);
                    await newResponseBody.CopyToAsync(originalBody);
                    sbInfor.AppendLine("Response:");
                    var responseText = await GetResponseContent(newResponseBody);
                    sbInfor.AppendLine(responseText);
                    int? retCode = GetRetCode(responseText);
                    if (retCode.HasValue && retCode > 0)
                    {
                        _logger.LogWarning(sbInfor.ToString());
                    }
                    else
                    {
                        _logger.LogInformation(sbInfor.ToString());
                    }
                }
            }
        }

        #region Private Methods
        private LoggingScope GetLoggingScope(HttpContext context)
        {
            //var action = context?.GetRouteData()?.Values?["action"]?.ToString();
            //if (action != null && _routes !=null && _routes.ContainsKey(action))
            //{
            //    return _routes[action];
            //}
            return LoggingScope.Both;
        }

        private static string Decode(string s)
        {
            return reUnicode.Replace(s, m =>
            {
                short c;
                if (short.TryParse(m.Groups[1].Value, System.Globalization.NumberStyles.HexNumber, 
                    CultureInfo.InvariantCulture, out c))
                {
                    return "" + (char)c;
                }
                return m.Value;
            });
        }

        public async Task<string> GetRequestContent(HttpRequest request)
        {
            request.EnableBuffering();
            using (var newRequestBody = _recyclableMemoryStreamManager.GetStream())
            {
                await request.Body.CopyToAsync(newRequestBody);
                request.Body.Seek(0, SeekOrigin.Begin);
                newRequestBody.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(newRequestBody, Encoding.UTF8))
                {
                    string text = await reader.ReadToEndAsync();
                    return Decode(text);
                }
            }
        }
        private async Task<string> GetResponseContent(MemoryStream responseBody)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(responseBody, Encoding.UTF8))
            {
                string text = await reader.ReadToEndAsync();
                return Decode(text);
            }
        }
        private int? GetRetCode(string responseText)
        {
            if (string.IsNullOrEmpty(responseText))
            {
                return null;
            }
            string startStr = "errcode\":\"";
            int startIndex = responseText.IndexOf(startStr);
            if (startIndex < 0)
            {
                return null;
            }
            int endIndex = responseText.IndexOf("\"", startIndex + startStr.Length);
            if (endIndex < 0)
            {
                return null;
            }
            string strRetCode = responseText.Substring(startIndex + startStr.Length, endIndex - startIndex - startStr.Length);
            int retCode;
            if (int.TryParse(strRetCode, out retCode))
            {
                return retCode;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }

    public enum LoggingScope
    {
        Both,
        RequestOnly
    }
}
