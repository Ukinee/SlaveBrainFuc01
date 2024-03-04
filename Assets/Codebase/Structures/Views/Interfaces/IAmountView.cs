using System;

namespace Codebase.Structures.Views.Interfaces
{
    [Obsolete("", true)]
    public interface IAmountView : IDisposable
    {
        public void Set(string value);
    }
}
