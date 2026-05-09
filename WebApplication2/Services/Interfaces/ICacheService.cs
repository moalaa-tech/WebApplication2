namespace CRM.WebApp.Services.Interfaces
{
    /// <summary>
    /// Service for caching frequently accessed lookup data to improve performance
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Gets cached data or retrieves and caches it if not present
        /// </summary>
        /// <typeparam name="T">Type of cached data</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="getItem">Function to retrieve data if not cached</param>
        /// <param name="expiration">Optional custom expiration time</param>
        /// <returns>Cached or retrieved data</returns>
        Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> getItem, TimeSpan? expiration = null);

        /// <summary>
        /// Gets cached data by key
        /// </summary>
        /// <typeparam name="T">Type of cached data</typeparam>
        /// <param name="key">Cache key</param>
        /// <returns>Cached data or null if not found</returns>
        T? Get<T>(string key);

        /// <summary>
        /// Sets data in cache with optional expiration
        /// </summary>
        /// <typeparam name="T">Type of data to cache</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="value">Data to cache</param>
        /// <param name="expiration">Optional custom expiration time</param>
        void Set<T>(string key, T value, TimeSpan? expiration = null);

        /// <summary>
        /// Removes data from cache
        /// </summary>
        /// <param name="key">Cache key to remove</param>
        void Remove(string key);

        /// <summary>
        /// Clears all cached data (use with caution)
        /// </summary>
        void Clear();

        /// <summary>
        /// Invalidates cache keys that start with the specified prefix
        /// </summary>
        /// <param name="prefix">Prefix to match for invalidation</param>
        void InvalidateByPrefix(string prefix);
    }
}