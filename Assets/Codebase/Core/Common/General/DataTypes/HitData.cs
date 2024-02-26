using UnityEngine;

namespace Codebase.Core.Common.General.DataTypes
{
    public struct HitData
    {
        public HitData(Vector3 position, Vector3 normal, bool isValid)
        {
            Position = position;
            Normal = normal;
            IsValid = isValid;
        }

        public Vector3 Position { get; }
        public Vector3 Normal { get; }
        public bool IsValid { get; }
    }
}