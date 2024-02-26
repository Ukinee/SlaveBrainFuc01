using System;
using System.Collections.Generic;

namespace Codebase.Core.Common.General.Extensions.ListExtentions
{
    public static partial class ListExtensions
    {
        public static List<T> GetRandomElements<T>(this List<T> list, int count) where T : class
        {
            List<T> result = new(count);
            Random random = new();

            for (int i = 0; i < count; i++)
            {
                if (list.Count == 0)
                    break;
                
                T randomElement = list.GetRandomElement(random);

                result.Add(randomElement);
            }

            return result;
        }
    }
}