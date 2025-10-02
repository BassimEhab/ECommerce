using DomainLayer.Exceptions;
using Shared.ErrorModels;
using System.Net;

namespace ECommerce.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            _next = Next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

                await HandleNotFoundEndPointAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");
                // Set Status Code For Response
                await HandleExceptionsAsync(context, ex);

            }
        }

        private static async Task HandleExceptionsAsync(HttpContext context, Exception ex)
        {
            // Response Object
            var Response = new ErrorToReturn
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = ex.Message
            };
            context.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedException => (int)HttpStatusCode.Unauthorized,
                BadRequestException badRequestException => GetBadRequestErrors(badRequestException, Response),
                _ => (int)HttpStatusCode.InternalServerError
            };
            // Return Object As JSON
            await context.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn response)
        {
            response.Errors = badRequestException.Errors;
            return (int)HttpStatusCode.BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext context)
        {
            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                var Response = new ErrorToReturn
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = $"End Point {context.Request.Path} Is Not Found"
                };
                await context.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
