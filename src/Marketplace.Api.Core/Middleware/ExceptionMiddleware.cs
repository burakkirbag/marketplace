using System;
using System.Net;
using System.Threading.Tasks;
using Marketplace.Api.Extensions;
using Marketplace.Api.Mvc.Models;
using Marketplace.Baskets;
using Marketplace.Baskets.Exceptions;
using Marketplace.Domain;
using Marketplace.Domain.Rules;
using Marketplace.Extensions;
using Marketplace.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using ApplicationException = Marketplace.Application.ApplicationException;

namespace Marketplace.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                LogHelper.AddError(httpContext.GetCorrelationId(), ex);

                var statusCode = GetStatusCode(ex);

                var data = new ApiReturn<object>()
                {
                    Code = (int) statusCode,
                    Success = false,
                    Message = ex.Message,
                    InternalMessage = statusCode == HttpStatusCode.InternalServerError ? ex.StackTrace : string.Empty
                };

                httpContext.Response.StatusCode = (int) statusCode;
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsync(data.ToJson());
            }
        }

        private HttpStatusCode GetStatusCode(Exception ex)
        {
            if (ex is BasketNotFoundException) return HttpStatusCode.NotFound;
            else if (ex is BasketItemNotFoundException) return HttpStatusCode.NotFound;
            else if (ex is BasketInvalidItemQuantityException) return HttpStatusCode.BadRequest;
            else if (ex is BusinessRuleValidationException) return HttpStatusCode.BadRequest;
            else if (ex is ApplicationException) return HttpStatusCode.NotAcceptable;
            else if (ex is DomainException) return HttpStatusCode.Conflict;
            else if (ex is MarketplaceException) return HttpStatusCode.NotFound;
            else return HttpStatusCode.InternalServerError;
        }
    }
}