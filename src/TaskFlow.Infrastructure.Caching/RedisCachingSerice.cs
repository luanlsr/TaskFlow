using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskFlow.Infrastructure.Caching.RedisCache
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = _redis.GetDatabase();
        }

        // Armazena um objeto no cache Redis com tempo de expiração
        public async Task SetCacheAsync(string key, object value, TimeSpan expiration)
        {
            var jsonData = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, jsonData, expiration);
        }

        // Recupera um objeto do cache Redis
        public async Task<T> GetCacheAsync<T>(string key)
        {
            var jsonData = await _database.StringGetAsync(key);
            if (jsonData.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<T>(jsonData);
        }

        // Remove um item do cache
        public async Task RemoveCacheAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        // Limpa o cache inteiro (usado com cuidado, pode afetar o desempenho)
        public async Task ClearCacheAsync()
        {
            var server = _redis.GetServer(_redis.GetEndPoints()[0]);
            await server.FlushDatabaseAsync();
        }
    }
}
