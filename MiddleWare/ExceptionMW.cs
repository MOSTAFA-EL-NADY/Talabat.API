using System.Text.Json;
using Talabat.API.Error;

namespace Talabat.API.MiddleWare
{
    public class ExceptionMW : IMiddleware
    {

        public ILogger<ExceptionMW> Logger { get; }
        public IHostEnvironment Host { get; }
        public ExceptionMW(ILogger<ExceptionMW> logger, IHostEnvironment host)
        {

            Logger = logger;
            Host = host;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = Host.IsDevelopment() ? new ExceptionResponse(500, ex.Message, ex.StackTrace)
                    : new ExceptionResponse(500, null, null);

                var jsonResponse = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
