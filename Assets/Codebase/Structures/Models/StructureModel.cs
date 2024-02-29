using System;

namespace Codebase.Structures.Models
{
    public class StructureModel
    {
        public StructureModel(int amount)
        {
            if(amount <= 0)
                throw new ArgumentException();
            
            Amount = amount;
            MaxAmount = amount;
        }

        public int Amount { get; private set; }
        public int MaxAmount { get; }
        
        public event Action<int> AmountChanged;

        public void Decrease()
        {
            if(Amount <= 0)
                throw new ArgumentException();
            
            Amount--;
            AmountChanged?.Invoke(Amount);
        }

        public void Dispose()
        {
            Amount = -1;
        }
    }
}
