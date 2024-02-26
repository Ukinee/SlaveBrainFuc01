using System.Collections.Generic;
using System.Linq;

namespace ApplicationCode.Core.Common.General.Extentions.ListExtentions
{
    public static partial class ListExtensions
    {
        public static (IEnumerable<T> added, IEnumerable<T> removed) Diff<T>
            (this IEnumerable<T> sourceCollection, IEnumerable<T> changedCollection)
        {
            IEnumerable<T> removed = sourceCollection.Except(changedCollection);
            IEnumerable<T> added = changedCollection.Except(sourceCollection);

            return (added, removed);
        }
        
        public static (IEnumerable<T> added, IEnumerable<T> removed) Diff<T>
            (this IReadOnlyCollection<T> sourceCollection, IReadOnlyCollection<T> changedCollection)
        {
            IEnumerable<T> removed = sourceCollection.Except(changedCollection);
            IEnumerable<T> added = changedCollection.Except(sourceCollection);

            return (added, removed);
        }
    }
}
