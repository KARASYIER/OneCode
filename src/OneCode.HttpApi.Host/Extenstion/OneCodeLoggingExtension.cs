using Microsoft.AspNetCore.Builder;
using OneCode.Middleware;
using System;
using System.Collections.Generic;

namespace OneCode.Extenstion
{
    public static class OneCodeLoggingExtension
    {

        public static IApplicationBuilder UseOneCodeLogging(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            return builder.UseMiddleware<LoggingMiddleware>();
        }

    }
}
