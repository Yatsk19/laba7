using System;
using System.Collections.Generic;

public class FunctionCache<TKey, TResult>
{
    private Dictionary<TKey, CacheItem> cache = new Dictionary<TKey, CacheItem>();
    private Func<TKey, TResult> function;
    private TimeSpan cacheDuration;

    public FunctionCache(Func<TKey, TResult> function, TimeSpan cacheDuration)
    {
        this.function = function;
        this.cacheDuration = cacheDuration;
    }

    public TResult GetResult(TKey key)
    {
        if (cache.TryGetValue(key, out var cacheItem) && DateTime.Now - cacheItem.Timestamp < cacheDuration)
        {
            return cacheItem.Result;
        }

        TResult result = function(key);
        cache[key] = new CacheItem(result, DateTime.Now);
        return result;
    }

    private class CacheItem
    {
        public TResult Result { get; }
        public DateTime Timestamp { get; }

        public CacheItem(TResult result, DateTime timestamp)
        {
            Result = result;
            Timestamp = timestamp;
        }
    }
}
