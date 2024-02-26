using System.Collections.Generic;

namespace Codebase.Core.Common.General.Extensions.ListExtentions
{
    public static partial class ListExtensions
    {
        public static List<T> GetRandomUniqueElements<T>(this IEnumerable<T> list, int count) where T : class
        {
            List<T> result = new(list);

            result = result.TakeRandomElements(count);

            return result;
        }
    }
}