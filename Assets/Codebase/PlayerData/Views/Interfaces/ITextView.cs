using System;

namespace Codebase.PlayerData.Views.Interfaces
{
    public interface ITextView : IDisposable
    {
        public void Set(string value);
    }
}
