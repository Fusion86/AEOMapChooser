using System;
using System.Collections.Generic;
using System.Linq;

namespace AEOMapChooser.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            Random r = new Random();
            var list = enumerable as IList<T> ?? enumerable.ToList();
            return list.Count == 0 ? default(T) : list[r.Next(0, list.Count)];
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            Random rnd = new Random();
            var temp = source.ToList();

            for (int i = 0; i < temp.Count; i++)
            {
                int j = rnd.Next(i, temp.Count);
                yield return temp[j];

                temp[j] = temp[i];
            }
        }
    }
}
