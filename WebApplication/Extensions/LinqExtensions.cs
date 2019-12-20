using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Extensions
{
    public static class LinqExtensions
    {
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }

        public static IEnumerable<List<T>> SplitList<T>(this IEnumerable<T> items, int nSize = 10)
        {
            if (items != null)
            {
                var itemList = items.ToList();
                var itemCount = itemList.Count;
                for (int i = 0; i < itemCount; i += nSize)
                {
                    yield return itemList.GetRange(i, Math.Min(nSize, itemCount - i));
                    //if ((itemCount - i) % nSize > 0)
                    //{
                    //    yield return itemList.GetRange(i, Math.Min(nSize + 1, itemCount - i));
                    //    i++;
                    //}
                    //else
                    //    yield return itemList.GetRange(i, Math.Min(nSize, itemCount - i));
                }
            }
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
        (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static TSource Select<TSource>(this TSource input, Action<TSource> updater)
        {
            updater(input);
            return input;
        }

        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            foreach (var element in source)
                target.Add(element);
        }
    }
}