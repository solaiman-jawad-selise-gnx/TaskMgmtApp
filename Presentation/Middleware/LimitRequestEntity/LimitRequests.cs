namespace Presentation.Middleware.LimitRequestEntity;

[AttributeUsage(AttributeTargets.Method)]
public class LimitRequests: Attribute
{
    public int TimeWindowInSeconds { get; set; }
    public int MaxRequestsInTimeWindow { get; set; }
    
}