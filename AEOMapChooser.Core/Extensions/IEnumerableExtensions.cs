using System;
using System.Collections.Generic;
using System.Linq;

namespace AEOMapChooser.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        private static Random random = new Random();

        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            var list = enumerable as IList<T> ?? enumerable.ToList();
            return list.Count == 0 ? default(T) : list[random.Next(0, list.Count)];
        }

        public static IEnumerable<T> RandomMany<T>(this IEnumerable<T> source)
        {
            var temp = source.ToList();

            for (int i = 0; i < temp.Count; i++)
            {
                int j = random.Next(i, temp.Count);
                yield return temp[j];

                temp[j] = temp[i];
            }
        }
    }
}
