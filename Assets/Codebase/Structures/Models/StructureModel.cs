using System;

namespace Codebase.Structures.Models
{
    public class StructureModel
    {
        public int Amount { get; private set; }
        public int MaxAmount { get; private set; }

        public event Action<int> AmountChanged;

        public void Add()
        {
            Amount++;
            AmountChanged?.Invoke(Amount);
            
            MaxAmount = Math.Max(MaxAmount, Amount);
        }

        public void Remove()
        {
            Amount--;
            AmountChanged?.Invoke(Amount);
        }

        public void Dispose()
        {
            Amount = -1;
            AmountChanged = null;
        }
    }
}
