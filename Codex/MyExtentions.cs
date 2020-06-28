using System;
using System.Collections.Generic;
using System.Linq;

namespace Codex
{
    public static class MyExtensions
    {
        private static Random random = new Random();

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> elements)
        {
            return elements.OrderBy(e => Guid.NewGuid());
        }

        public static T Pick<T>(this IList<T> elements)
        {
            return elements[random.Next(0, elements.Count - 1)];
        }

        public static IList<T> Pick<T>(this IList<T> elements, int count)
        {
            return Enumerable.Range(0, count).Select(_ => elements[random.Next(0, elements.Count - 1)]).ToList();
        }

    }
}
