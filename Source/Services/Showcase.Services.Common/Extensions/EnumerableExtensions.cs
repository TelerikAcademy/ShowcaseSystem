namespace Showcase.Services.Common.Extensions
{
    using System;
    using System.Collections.Generic;
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
            var tasks = new List<Task>();

            foreach (var item in enumerable)
            {
                tasks.Add(Task.Run(() => { action(item); }));
            }

            await Task.WhenAll(tasks);
        }

        public static async Task<IEnumerable<TResult>> ForEachAsync<T, TResult>(this IEnumerable<T> enumerable, Func<T, Task<TResult>> func)
        {
            var tasks = new List<Task<TResult>>();

            foreach (var item in enumerable)
            {
                tasks.Add(Task.Run<TResult>(() => { return func(item); }));
            }

            return await Task.WhenAll(tasks);
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
            return new HashSet<T>(enumerable);
        }
    }
}