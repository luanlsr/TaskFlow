namespace TaskFlow.Infrastructure.Caching
{
    public interface ICacheService
    {
        Task SetCacheAsync(string key, object value, TimeSpan expiration);
        Task<T> GetCacheAsync<T>(string key);
        Task RemoveCacheAsync(string key);
        Task ClearCacheAsync();
    }
}
