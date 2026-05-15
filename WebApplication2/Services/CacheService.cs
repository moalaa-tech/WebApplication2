using CRM.WebApp.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace CRM.WebApp.Services
{
    /// <summary>
    /// Service implementation for caching frequently accessed lookup data
    /// </summary>
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CacheService> _logger;
        private readonly ConcurrentDictionary<string, byte> _cacheKeys = new();
        private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(30);

        public CacheService(IMemoryCache memoryCache, ILogger<CacheService> logger)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> getItem, TimeSpan? expiration = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Cache key cannot be null or empty", nameof(key));

            if (_memoryCache.TryGetValue(key, out T? cachedValue))
            {
                _logger.LogDebug("Cache hit for key: {CacheKey}", key);
                return cachedValue;
            }

            _logger.LogDebug("Cache miss for key: {CacheKey}. Fetching data...", key);
            
            try
            {
                var value = await getItem();
                if (value != null)
                {
                    Set(key, value, expiration);
                }
                return value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching data for cache key: {CacheKey}", key);
                throw;
            }
        }

        public T? Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return default;

            return _memoryCache.TryGetValue(key, out T? value) ? value : default;
        }

        public void Set<T>(string key, T value, TimeSpan? expiration = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Cache key cannot be null or empty", nameof(key));

            var cacheExpiration = expiration ?? _defaultExpiration;
            
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheExpiration,
                SlidingExpiration = TimeSpan.FromMinutes(10), // Refresh if accessed within 10 minutes
                Priority = CacheItemPriority.High,
                Size = 1 // Assuming each cached item has size of 1
            };

            // Add post-eviction callback for logging
            cacheEntryOptions.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                _cacheKeys.TryRemove(key.ToString()!, out _);
                _logger.LogDebug("Cache item evicted: {CacheKey}, Reason: {EvictionReason}", key, reason);
            });

            _memoryCache.Set(key, value, cacheEntryOptions);
            _cacheKeys.TryAdd(key, 0);
            
            _logger.LogDebug("Cached item with key: {CacheKey}, Expiration: {Expiration}", key, cacheExpiration);
        }

        public void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;

            _memoryCache.Remove(key);
            _cacheKeys.TryRemove(key, out _);
            _logger.LogDebug("Removed cache item with key: {CacheKey}", key);
        }

        public void Clear()
        {
            _logger.LogWarning("Clearing all cached data. This will impact performance until cache is rebuilt.");
            
            foreach (var key in _cacheKeys.Keys.ToList())
            {
                _memoryCache.Remove(key);
            }
            
            _cacheKeys.Clear();
        }

        public void InvalidateByPrefix(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                return;

            var keysToRemove = _cacheKeys.Keys
                .Where(key => key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var key in keysToRemove)
            {
                Remove(key);
            }

            _logger.LogDebug("Invalidated {Count} cache items with prefix: {Prefix}", keysToRemove.Count, prefix);
        }
    }

    /// <summary>
    /// Extension methods for common cache operations
    /// </summary>
    public static class CacheExtensions
    {
        // Cache key prefixes for different data types
        public const string COUNTRIES_KEY = "lookup:countries";
        public const string STATES_KEY = "lookup:states";
        public const string CITIES_KEY = "lookup:cities";
        public const string BUSINESSES_KEY = "lookup:businesses";
        public const string CURRENCIES_KEY = "lookup:currencies";
        public const string PRODUCT_TYPES_KEY = "lookup:product-types";
        public const string DEPARTMENTS_KEY = "lookup:departments";
        
        /// <summary>
        /// Gets states for a specific country with caching
        /// </summary>
        public static string GetStatesKey(int countryId) => $"{STATES_KEY}:country:{countryId}";
        
        /// <summary>
        /// Gets cities for a specific state with caching
        /// </summary>
        public static string GetCitiesKey(int stateId) => $"{CITIES_KEY}:state:{stateId}";

        /// <summary>
        /// Gets cache key for all cities.
        /// </summary>
        public static string GetAllCitiesKey() => CITIES_KEY;
        
        /// <summary>
        /// Gets cache key for active lookup data
        /// </summary>
        public static string GetActiveLookupKey(string baseKey) => $"{baseKey}:active";
    }
}
