namespace Presentation.Middleware.LimitRequestEntity;

public class RequestStatistics
{
    public RequestStatistics(
        DateTime lastResponse,
        int numberOfRequests)
    {
        LastResponse = lastResponse;
        NumberOfRequests = numberOfRequests;
    }

    public DateTime LastResponse { get; private set; }
    public int NumberOfRequests { get; private set; }

    public bool HasConsumedAllRequests(int timeWindowInSeconds, int maxRequests)
        => DateTime.UtcNow < LastResponse.AddSeconds(timeWindowInSeconds) && NumberOfRequests >= maxRequests;

    public void IncrementRequestCount(int maxRequests)
    {
        LastResponse = DateTime.UtcNow;

        if (NumberOfRequests == maxRequests)
            NumberOfRequests = 1;

        else
            NumberOfRequests++;
    } 
}