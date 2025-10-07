using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System.Text;

namespace Presentation.Attributes
{
    internal class CacheAttribute(int DurationInSec = 90) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string CacheKey = CreateCacheKey(context.HttpContext.Request);
            // Search For Value With Cache Key
            ICacheService CacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var CacheValue = await CacheService.GetAsync(CacheKey);
            // Retrun Value If Not Null
            if (CacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = CacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }
            // Return Value If Is Null
            var ExcecutedContext = await next.Invoke();
            // Set Value With Cache Key.
            if (ExcecutedContext.Result is OkObjectResult result)
            {
                await CacheService.SetAsync(CacheKey, result.Value, TimeSpan.FromSeconds(DurationInSec));
            }

        }

        private string CreateCacheKey(HttpRequest request)
        {
            // {{BaseUrl}}/api/Products?BrandId=1&TypeId=1
            StringBuilder Key = new StringBuilder();
            Key.Append($"{request.Path}?");
            foreach (var item in request.Query.OrderBy(q => q.Key))
            {
                Key.Append($"{item.Key}={item.Value}&");
            }
            return Key.ToString();
        }
    }
}
