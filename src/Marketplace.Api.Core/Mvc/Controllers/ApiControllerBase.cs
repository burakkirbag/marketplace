﻿using System.Collections.Generic;
using System.Linq;
using Marketplace.Api.Extensions;
using Marketplace.Api.Mvc.Models;
using Marketplace.Logging;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Marketplace.Api.Mvc.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApiControllerBase : Controller
    {
        [NonAction]
        protected IActionResult Success<T>(string message, string internalMessage, T data) =>
            Ok(new ApiReturn<T>
                {Code = 200, Success = true, Data = data, Message = message, InternalMessage = internalMessage});

        [NonAction]
        protected IActionResult Success(string message, string internalMessage) =>
            Ok(new ApiReturn {Code = 200, Success = true, Message = message, InternalMessage = internalMessage});

        [NonAction]
        protected IActionResult Created<T>(string message, string internalMessage, T data) =>
            StatusCode(201, new ApiReturn<T>
            {
                Code = 200,
                Success = true,
                Data = data,
                Message = message
            });

        [NonAction]
        protected IActionResult BadRequest<T>(string message, string internalMessage, T data)
        {
            LogHelper.AddWarning(HttpContext.GetCorrelationId(), message, internalMessage);

            return StatusCode(400,
                new ApiReturn
                    {Code = 400, Success = false, Data = data, Message = message, InternalMessage = internalMessage});
        }

        [NonAction]
        protected IActionResult BadRequest(string message, string internalMessage)
        {
            LogHelper.AddWarning(HttpContext.GetCorrelationId(), message, internalMessage);

            return StatusCode(400,
                new ApiReturn
                {
                    Code = 400, Success = false, Message = message, InternalMessage = internalMessage
                });
        }

        [NonAction]
        protected IActionResult Unauthorized<T>(string message, string internalMessage, T data) =>
            StatusCode(401,
                new ApiReturn<T>
                {
                    Code = 401, Success = false, Data = data, Message = message, InternalMessage = internalMessage
                });

        [NonAction]
        protected IActionResult Forbidden<T>(string message, string internalMessage, T data) => StatusCode(403,
            new ApiReturn<T>
            {
                Code = 403, Success = false, Data = data, Message = message, InternalMessage = internalMessage
            });

        [NonAction]
        protected IActionResult NotFound<T>(string message, string internalMessage, T data) => StatusCode(404,
            new ApiReturn<T>
            {
                Code = 404, Success = false, Data = data, Message = message, InternalMessage = internalMessage
            });

        [NonAction]
        protected IActionResult Error<T>(string message, string internalMessage, string[] errors, T data) => StatusCode(
            500,
            new ApiReturn<T>
            {
                Code = 500, Success = false, Data = data, Message = message, InternalMessage = internalMessage,
                Errors = errors?.ToList()
            });
    }
}