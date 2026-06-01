using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Telecomm360.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var responseCode = HttpStatusCode.InternalServerError;

            if (exception is KeyNotFoundException) responseCode = HttpStatusCode.NotFound;
            else if (exception is UnauthorizedAccessException) responseCode = HttpStatusCode.Unauthorized;
            else if (exception is ArgumentException) responseCode = HttpStatusCode.BadRequest;

            context.Response.StatusCode = (int)responseCode;
            
            // 🛠️ FIXED: We are now grabbing the InnerException so we can see what SQL Server is complaining about!
            var payload = JsonSerializer.Serialize(new 
            { 
                errorMessage = exception.Message,
                sqlSecretMessage = exception.InnerException?.Message 
            });
            
            return context.Response.WriteAsync(payload);
        }
    }
}