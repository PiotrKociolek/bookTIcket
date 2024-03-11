using System.Net;
using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Core.Exceptions;
using Template.Modules.Shared.Infrastructure.Framework.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Template.Modules.Shared.Infrastructure.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionHandler _exceptionHandler;

        public ExceptionHandlerMiddleware(
            RequestDelegate next, 
            IExceptionHandler exceptionHandler)
        {
            _next = next;
            _exceptionHandler = exceptionHandler;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await ProcessException(context, exception);
            }
        }

        private Task ProcessException(HttpContext context, Exception exception)
        {
            var remoteIpAddress = context.GetRemoteIpAddress();
            var response = _exceptionHandler.HandleError(exception, remoteIpAddress);

            return Complete(context, response.ErrorResponse, response.HttpStatusCode);
        }

        private Task Complete(HttpContext context, ErrorResponse response, HttpStatusCode code)
        {
            var payload = JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(payload);
        }
    }
}