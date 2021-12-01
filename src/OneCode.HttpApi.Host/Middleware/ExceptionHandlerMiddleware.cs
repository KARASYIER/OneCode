using Microsoft.AspNetCore.Http;
using OneCode.ToolKit.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Validation;

namespace OneCode.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (EntityNotFoundException)
            {
                await HandlerAsync(context, -1, $"没有查询到相关数据");
            }
            catch (OneCodeBizException ex)
            {
                await HandlerAsync(context, ex.ErrorCode, ex.Message);
            }
            catch (AbpValidationException)
            {
                await HandlerAsync(context, 9001, OneCodeDomainErrorCodes.ErrMsg_9001);
            }
            catch (Exception ex)
            {
                await HandlerAsync(context, -1, ex.Message, ex.StackTrace);
            }
            finally
            {

            }
        }

        private async Task HandlerAsync(HttpContext context, int code = -1, string msg = "", object detail = null)
        {
            context.Response.ContentType = "application/json;charset=utf-8";
            string ret = JsonSerializer.Serialize(ResponseReturn.ReturnFailureAsync(code, msg, detail)?.Result);
            await context.Response.WriteAsync(ret);
        }
    }
}
