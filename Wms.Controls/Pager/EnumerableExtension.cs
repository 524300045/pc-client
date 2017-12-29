using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wms.Controls.Pager
{
    public static class EnumerableExtension
    {
        public static IList<T> ToIList<T>(
#if NET_3_5
			this
#endif
IEnumerable<T> enumerable)
        {
            return enumerable is IList<T> ? (IList<T>)enumerable : new List<T>(enumerable);
        }

        public static List<T> ToList<T>(
#if NET_3_5
			this
#endif
IEnumerable<T> enumerable)
        {
            return enumerable is List<T> ? (List<T>)enumerable : new List<T>(enumerable);
        }
    }
}
