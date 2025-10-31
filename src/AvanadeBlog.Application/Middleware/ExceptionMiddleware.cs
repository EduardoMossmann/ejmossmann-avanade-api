using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace AvanadeBlog.Application.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                string message;
                HttpStatusCode statusCode;
                object? value = null;

                switch (ex)
                {
                    case HttpResponseException httpResponseException:
                        statusCode = (HttpStatusCode)httpResponseException.StatusCode;
                        message = httpResponseException.Message;
                        value = httpResponseException.Value;
                        break;
                    default:
                        statusCode = HttpStatusCode.InternalServerError;
                        message = ex.Message;
                        break;
                }

                Log.Information("Request failed with Status Code: {StatusCode}. Message: {ExceptionMessage}", statusCode, message);

                httpContext.Response.StatusCode = (int) statusCode;
                httpContext.Response.ContentType = "application/json";
                var errorResponse = new
                {
                    status = statusCode,
                    message,
                    value 
                };

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
        }
    }
}
