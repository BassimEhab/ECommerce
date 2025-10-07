namespace ServiceAbstraction
{
    public interface ICacheService
    {
        // Get
        Task<string?> GetAsync(string CacheKey);
        // Set
        Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive);
    }
}
