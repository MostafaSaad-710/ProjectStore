using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class CacheAttribute(int timeInSeconds) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //logic for caching
            var cacheServices = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().cacheService;

            // GenerAte a cache key based on the request path and query string
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var result = await cacheServices.GetAsync(cacheKey);

            if (!string.IsNullOrEmpty(result))
            {
                // If a cached response exists, return it immediately
                var response = new  ContentResult
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                context.Result = response;
                return;
            }

            var actionContext = await next.Invoke(); // Continue to the action method
            if(actionContext.Result is ObjectResult objectResult)
            {
                // Cache the response for future requests
                await cacheServices.SetAsync(cacheKey, objectResult.Value, TimeSpan.FromSeconds(timeInSeconds));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");

            foreach (var item in request.Query)
            {
                keyBuilder.Append($"|{item.Key}-{item.Value}");
            }
            return keyBuilder.ToString();
        }
    }
}
