using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.TinyGrammar.Core.Helpers
{
    static class ListExtensions
    {
        private static Random Rnd = new Random();
        public static T PickRandom<T>(this List<T> list)
        {
            if (list.Count == 0) return default(T);

            return list[Rnd.Next(list.Count)];
        }
    }

    static class StringExtensions
    {
        public static string ReplaceFirst(this string original, string search, string replace)
        {
            var pos = original.IndexOf(search);
            if (pos < 0)
                return original;

            return original.Substring(0, pos) + replace + original.Substring(pos + search.Length);
        }
    }
}
