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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");
                // Set Status Code For Response
                context.Response.StatusCode = ex switch
                {
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                };
                // Response Object
                var Response = new ErrorToReturn
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message
                };
                // Return Object As JSON
                await context.Response.WriteAsJsonAsync(Response);

            }
        }

    }
}
