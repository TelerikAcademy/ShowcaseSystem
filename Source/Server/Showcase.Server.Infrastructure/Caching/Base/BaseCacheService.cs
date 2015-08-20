namespace Showcase.Server.Infrastructure.Caching.Base
{
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Caching;

    public abstract class BaseCacheService
    {
        protected async Task<T> Get<T>(
            string cacheId,
            Func<Task<T>> getItemCallback,
            DateTime? absoluteExpiration = null,
            TimeSpan? slidingExpiration = null) 
            where T : class
        {
            var item = HttpRuntime.Cache.Get(cacheId) as T;
            if (item == null)
            {
                item = await getItemCallback();
                HttpContext.Current.Cache.Insert(
                    cacheId,
                    item,
                    null,
                    absoluteExpiration ?? Cache.NoAbsoluteExpiration,
                    slidingExpiration ?? Cache.NoSlidingExpiration); // TODO: lock
                return item;
            }

            return item;
        }
    }
}
