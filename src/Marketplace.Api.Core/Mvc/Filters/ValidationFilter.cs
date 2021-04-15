using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Marketplace.Api.Extensions;
using Marketplace.Api.Mvc.Models;
using Marketplace.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Marketplace.Api.Mvc.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var modelError in context.ModelState)
                {
                    foreach (var error in modelError.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                var result = new ApiReturn<List<string>>
                {
                    Code = 400,
                    Success = false,
                    Message = "Lütfen girmiş olduğunuz bilgileri kontrol ediniz.",
                    Errors = errors
                };

                context.Result = new BadRequestObjectResult(result);

                LogHelper.AddWarning(context.HttpContext.GetCorrelationId(), result.Message, result.InternalMessage,
                    errors);
            }
        }
    }
}