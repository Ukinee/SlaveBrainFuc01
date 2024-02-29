using System;
using System.Collections.Generic;
using Codebase.Cubes.Models;

namespace Codebase.Structures.Models
{
    public class StructureModel
    {
        public int Amount { get; private set; }
        public event Action<int> AmountChanged;

        public void Add()
        {
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
            Amount = -1;
            AmountChanged = null;
        }
    }
}
