using Assets.Codebase.Core.Frameworks.SignalSystem.Interfaces;
using UnityEngine;

namespace Codebase.Structures.Controllers.Signals
{
    public class CreateStructureSignal : ISignal
    {
        public string StructureName { get; }
        public Vector3 Position { get; }

        public CreateStructureSignal(string structureName, Vector3 position)
        {
            StructureName = structureName;
            Position = position;
        }
    }
}
