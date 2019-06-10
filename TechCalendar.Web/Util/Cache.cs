using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechCalendar.Web.Util
{
    public class Cache<TRequest, TCacheItemType>
    {
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
            var now = DateTime.Now;

            if ((now - cacheItem.Creation).TotalMilliseconds > _ttl)
            {
                _cache[request] = null;
                return default(TCacheItemType);
            }

            return _cache[request].Item;
        }

        public void Put(TRequest request, TCacheItemType item)
        {
            _cache[request] = new CacheItem<TCacheItemType>(item);
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
