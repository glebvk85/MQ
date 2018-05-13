using System.Runtime.Caching;

namespace MetaQuotes.Providers
{
    public class MemCacheProvider<T> where T : class
    {
        public T TryGetResponse(string key)
        {
            ObjectCache cache = MemoryCache.Default;
            return cache[key] as T;
        }

        public void SaveResponse(string key, T response)
        {
            ObjectCache cache = MemoryCache.Default;
            var policy = new CacheItemPolicy();
            cache.Set(key, response, policy);
        }
    }
}