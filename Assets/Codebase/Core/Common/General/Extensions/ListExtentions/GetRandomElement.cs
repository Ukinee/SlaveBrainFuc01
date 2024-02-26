using System;
using System.Collections.Generic;
using Codebase.Core.Common.General.Utils;

namespace Codebase.Core.Common.General.Extensions.ListExtentions
{
    public static partial class ListExtensions
    {
        public static T GetRandomElement<T>(this List<T> list)
        {
            Random random = new();

            return list.GetRandomElement(random);
        }

        public static T GetRandomElement<T>(this List<T> list, Random random)
        {
            if (list.Count == 0)
                MaloyAlert.Error("List is empty");
            
            return list[random.Next(list.Count)];
        }
    }
}