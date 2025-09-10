﻿using LibraryManagementSystem.Application.Service;
using StackExchange.Redis;
using System.Text.Json;
namespace LibraryManagementSystem.Infrastructure
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _database;
        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, json, expiry);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.IsNullOrEmpty) return default;
            return JsonSerializer.Deserialize<T>(value!);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
            
        }
    }
}
