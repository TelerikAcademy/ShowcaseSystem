namespace Showcase.Services.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            var tasks = enumerable
                .Select(item => Task.Run(() => action(item)))
                .ToList();

            await Task.WhenAll(tasks);
        }

        public static async Task<IEnumerable<TResult>> ForEachAsync<T, TResult>(this IEnumerable<T> enumerable, Func<T, Task<TResult>> func)
        {
            var tasks = enumerable
                .Select(item => Task.Run(() => func(item)))
                .ToList();

            return await Task.WhenAll(tasks);
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
            return new HashSet<T>(enumerable);
        }
    }
}