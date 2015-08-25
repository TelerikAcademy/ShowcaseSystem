namespace Showcase.Server.Infrastructure.Caching.Base
{
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Caching;

    public abstract class BaseCacheService
    {
        private const int DefaultAbsoluteExpirationInMinutes = 15;

        protected DateTime DefaultAbsoluteExpiration
        {
            get
            {
                return DateTime.Now.AddMinutes(DefaultAbsoluteExpirationInMinutes);
            }
        }

        protected async Task<T> Get<T>(
            string cacheId,
            Func<Task<T>> getItemCallback,
            DateTime? absoluteExpiration = null,
            TimeSpan? slidingExpiration = null)
            where T : class
        {
            var item = this.GetFromCache<T>(cacheId);
            if (item == null)
            {
                item = await getItemCallback();
                HttpContext.Current.Cache.Insert(
                    cacheId,
                    item,
                    null,
                    absoluteExpiration ?? Cache.NoAbsoluteExpiration,
                    slidingExpiration ?? Cache.NoSlidingExpiration);
            }

            return item;
        }

        private T GetFromCache<T>(string cacheId) where T : class
        {
            return HttpRuntime.Cache.Get(cacheId) as T;
        }
    }
}
