using Microsoft.AspNetCore.Http;
using OneCode.ToolKit.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace OneCode.ToolKit.Middleware
{
    //public class ExceptionHandlerMiddleware
    //{
    //    private readonly RequestDelegate _next;

    //    public ExceptionHandlerMiddleware(RequestDelegate next)
    //    {
    //        _next =next;
    //    }

    //    public async Task Invoke(HttpContext context)
    //    {
    //        try
    //        {
    //            await _next(context);
    //        }
    //        catch (Exception ex)
    //        {
    //            await HandlerAsync(context, ex.Message,ex.StackTrace);
    //        }
    //        finally
    //        {
                
    //        }
    //    }

    //    private async Task HandlerAsync(HttpContext context,string msg,object detail)
    //    {
    //        context.Response.ContentType = "application/json;charset=utf-8";
    //        string ret=JsonSerializer.Serialize(ResponseReturn.ReturnFailureAsync("-1", msg, detail)?.Result);
    //        await context.Response.WriteAsync(ret);
    //    }
    //}
}
