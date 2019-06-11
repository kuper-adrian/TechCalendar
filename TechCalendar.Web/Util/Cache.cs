using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechCalendar.Web.Util
{
    public class Cache<TRequest, TCacheItemType>
    {
        private object _cacheLock = new object();
        private long _ttl;
        private Dictionary<TRequest, CacheItem<TCacheItemType>> _cache;

        public Cache(long ttl = 3_600_000 /* 1 hour */)
        {
            _ttl = ttl;
            _cache = new Dictionary<TRequest, CacheItem<TCacheItemType>>();
        }

        public TCacheItemType Get(TRequest request)
        {
            if (!_cache.ContainsKey(request))
            {
                return default(TCacheItemType);
            }

            var cacheItem = _cache[request];

            if ((DateTime.Now - cacheItem.Creation).TotalMilliseconds > _ttl)
            {
                _cache.Remove(request);
                return default(TCacheItemType);
            }

            return cacheItem.Item;
        }

        public void Put(TRequest request, TCacheItemType item)
        {
            lock (_cacheLock)
            {
                _cache.Add(request, new CacheItem<TCacheItemType>(item));
            }
        }

        private void Remove(TRequest request)
        {
            lock (_cacheLock)
            {
                _cache.Remove(request);
            }
        }
    }

    internal class CacheItem<TCacheItemType>
    {
        public DateTime Creation { get; private set; }
        public TCacheItemType Item { get; private set; }

        public CacheItem(TCacheItemType item)
        {
            Creation = DateTime.Now;
            Item = item;
        }
    }
}
