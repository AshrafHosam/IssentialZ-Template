using Application.Response;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace API.Middlewares
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationException(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGenericException(context, ex);
            }
        }

        private Task HandleValidationException(HttpContext context, ValidationException ex)
        {
            _logger.LogError(ex.Message);

            var result = ex.Errors.Select(a => a.ErrorMessage).ToList();

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }

        private Task HandleGenericException(HttpContext context, Exception ex)
        {
            _logger.LogError(ex.Message);

            var result = new BaseResponse<object>
            {
                Errors = new List<string>
                {
                    ex.Message,
                    ex.InnerException?.Message ?? string.Empty,
                    ex.StackTrace ?? string.Empty
                },
                Data = ex.Message
            };

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}
