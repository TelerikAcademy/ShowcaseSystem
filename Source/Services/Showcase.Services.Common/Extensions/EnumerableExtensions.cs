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

        public static IEnumerable<Task> ForEachAsync<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            var tasks = new List<Task>();

            foreach (var item in enumerable)
            {
                tasks.Add(Task.Run(() => { action(item); }));
            }

            return tasks;
        }
    }
}
