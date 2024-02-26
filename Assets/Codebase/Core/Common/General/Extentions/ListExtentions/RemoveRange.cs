using System.Collections.Generic;

namespace ApplicationCode.Core.Common.General.Extentions.ListExtentions
{
    public static partial class ListExtensions
    {
        public static void RemoveRange<T>(this List<T> list, IEnumerable<T> items)
        {
            foreach (T item in items)
                list.Remove(item);
        }
    }
}