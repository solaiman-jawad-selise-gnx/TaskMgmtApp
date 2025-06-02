using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Presentation.Middleware.LimitRequestEntity;

namespace Presentation.Middleware;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDistributedCache _cache;
    private readonly ILogger<RateLimitingMiddleware> _logger;

    public RateLimitingMiddleware(RequestDelegate next, IDistributedCache cache, ILogger<RateLimitingMiddleware> logger)
    {
        _next = next;
        _cache = cache;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        LimitRequests? limitRequestsAttribute = context.GetEndpoint()?.Metadata.GetMetadata<LimitRequests>();
        if (limitRequestsAttribute == null)
        {
            await _next(context);
            return;
        }
        var key = $"{context.Request.Method}:{context.Request.Path}";
        var consumptionData = await _cache.GetStringAsync(key);
        RequestStatistics? stats;
        if (consumptionData != null)
        { 
            stats = JsonConvert.DeserializeObject<RequestStatistics>(consumptionData);
            if (stats == null)
            {
                throw new InvalidOperationException("Failed to deserialize request statistics.");
            }
            
            if (stats.HasConsumedAllRequests(limitRequestsAttribute.TimeWindowInSeconds, 
                    limitRequestsAttribute.MaxRequestsInTimeWindow)) 
            {
                _logger.LogWarning("Rate limit exceeded for key: {Key}", key);
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                return;
            }
            stats.IncrementRequestCount(limitRequestsAttribute.MaxRequestsInTimeWindow);
            _logger.LogDebug("Incrementing request count for key: {Key}. Cache Entry: {stats}", key, stats.ToString());
        }
        else
        {
            stats = new RequestStatistics(DateTime.UtcNow, 1);
            _logger.LogDebug("Creating new request statistics for key: {Key}. Cache Entry: {stats}", key, stats.ToString());
        }
        await _cache.SetStringAsync(key, JsonConvert.SerializeObject(stats));
        
        await _next(context);
    }
    
}