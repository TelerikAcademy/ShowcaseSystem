namespace Showcase.Server.Infrastructure.Caching.Base
{
    using System;
    using System.Threading.Tasks;
    using System.Web;

    public abstract class BaseCacheService
    {
        protected async Task<T> Get<T>(string cacheId, Func<Task<T>> getItemCallback) where T : class
        {
            var item = HttpRuntime.Cache.Get(cacheId) as T;
            if (item == null)
            {
                item = await getItemCallback();
                HttpContext.Current.Cache.Insert(cacheId, item); // TODO: lock, add optional timespan
                return item;
            }

            return item;
        }
    }
}
