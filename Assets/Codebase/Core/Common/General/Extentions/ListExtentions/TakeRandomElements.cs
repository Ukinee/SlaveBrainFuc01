using System;
using System.Collections.Generic;

namespace ApplicationCode.Core.Common.General.Extentions.ListExtentions
{
    public static partial class ListExtensions
    {
        public static List<T> TakeRandomElements<T>(this List<T> list, int count) where T : class
        {
            Random random = new();
            List<T> result = new(count);
            
            for (int i = 0; i < count; i++)
            {
                if (list.Count == 0)
                    break;
                
                T element = list.GetRandomElement(random);
                
                list.Remove(element);
                result.Add(element);
            }

            return result;
        }
    }
}
