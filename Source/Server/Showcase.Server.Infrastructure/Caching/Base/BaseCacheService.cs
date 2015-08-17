namespace Showcase.Server.Infrastructure.Caching.Base
{
    using System;
    using System.Web;

    public abstract class BaseCacheService
    {
        protected T Get<T>(string cacheId, Func<T> getItemCallback) where T : class
        {
            var item = HttpRuntime.Cache.Get(cacheId) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpContext.Current.Cache.Insert(cacheId, item);
                return item;
            }

            return item;
        }
    }
}
