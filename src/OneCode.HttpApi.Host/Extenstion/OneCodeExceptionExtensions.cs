using Microsoft.AspNetCore.Builder;
using OneCode.Middleware;
using System;

namespace OneCode.Extenstion
{
    public static class OneCodeExceptionExtensions
    {

        public static IApplicationBuilder UseOneCodeExceptionHandler(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
