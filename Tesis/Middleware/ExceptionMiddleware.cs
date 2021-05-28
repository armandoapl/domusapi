using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Tesis.Errors;

namespace Tesis.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next, 
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            /*
             Now this what happens I will add this Middleware class ate the topo of the middlewares in the configureMethod
             so whenever a exception occurs in any part of the middlewares in the app, if there (where the exception occurs) is not
             any middleware handlinig the it's gonna throw the exception up and up until they reach something that can handles the exception, 
             to this middleware (because is the first one) and by doing this I'm going to be handling exceptions globally rather than locally
             */
            try
            {
                
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var jsonResponse = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(jsonResponse);

            }
        }
    }
}
