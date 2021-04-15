using System;
using Microsoft.AspNetCore.Http;

namespace Marketplace.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetCorrelationId(this HttpContext httpContext)
        {
            if (httpContext.Request != null && httpContext.Request.Headers.ContainsKey("X-Correlation-Id"))
                return httpContext.Request.Headers["X-Correlation-Id"];

            return Guid.NewGuid().ToString();
        }
    }
}