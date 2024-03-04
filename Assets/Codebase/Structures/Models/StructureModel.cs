using System;
using Codebase.Core.Common.General.Extensions.ObjectExtensions;

namespace Codebase.Structures.Models
{
    public class StructureModel
    {
        public int Amount { get; private set; }
        public int MaxAmount { get; private set; }

        public event Action<int> AmountChanged;
        public event Action Disposed;

        public void Add()
        {
            MaxAmount = Math.Max(Amount, MaxAmount);
            Amount++;
            AmountChanged?.Invoke(Amount);
        }

        public void Remove()
        {
            Amount--;
            AmountChanged?.Invoke(Amount);
        }

        public void Dispose()
        {
            $"Disposing Structure Model".Log();
            Disposed?.Invoke();
        }
    }
}
