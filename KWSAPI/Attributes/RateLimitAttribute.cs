using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace KWSAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class RateLimitAttribute : ActionFilterAttribute
    {
        private readonly int _maxRequests;
        private readonly TimeSpan _timeWindow;
        private readonly string _policyName;

        public RateLimitAttribute(int maxRequests = 60, int timeWindowInSeconds = 60, string policyName = "default")
        {
            _maxRequests = maxRequests;
            _timeWindow = TimeSpan.FromSeconds(timeWindowInSeconds);
            _policyName = policyName;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var memoryCache = context.HttpContext.RequestServices.GetService<IMemoryCache>();
            if (memoryCache == null)
            {
                await next();
                return;
            }

            var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var endpoint = context.HttpContext.Request.Path.ToString();
            var cacheKey = $"rate_limit_{_policyName}_{ipAddress}_{endpoint}";

            if (memoryCache.TryGetValue<RequestCounter>(cacheKey, out var counter))
            {
                if (counter.Count >= _maxRequests)
                {
                    context.Result = new ContentResult
                    {
                        Content = "Rate limit exceeded. Please try again later.",
                        StatusCode = (int)HttpStatusCode.TooManyRequests,
                        ContentType = "text/plain"
                    };
                    
                    context.HttpContext.Response.Headers.Add("X-RateLimit-Limit", _maxRequests.ToString());
                    context.HttpContext.Response.Headers.Add("X-RateLimit-Remaining", "0");
                    context.HttpContext.Response.Headers.Add("X-RateLimit-Reset", counter.ResetTime.ToUnixTimeSeconds().ToString());
                    
                    return;
                }

                counter.Count++;
                memoryCache.Set(cacheKey, counter, counter.ResetTime);
            }
            else
            {
                var newCounter = new RequestCounter
                {
                    Count = 1,
                    ResetTime = DateTimeOffset.UtcNow.Add(_timeWindow)
                };
                memoryCache.Set(cacheKey, newCounter, _timeWindow);
            }

            var currentCounter = memoryCache.Get<RequestCounter>(cacheKey);
            if (currentCounter != null)
            {
                context.HttpContext.Response.Headers.Add("X-RateLimit-Limit", _maxRequests.ToString());
                context.HttpContext.Response.Headers.Add("X-RateLimit-Remaining", (_maxRequests - currentCounter.Count).ToString());
                context.HttpContext.Response.Headers.Add("X-RateLimit-Reset", currentCounter.ResetTime.ToUnixTimeSeconds().ToString());
            }

            await next();
        }

        private class RequestCounter
        {
            public int Count { get; set; }
            public DateTimeOffset ResetTime { get; set; }
        }
    }
}