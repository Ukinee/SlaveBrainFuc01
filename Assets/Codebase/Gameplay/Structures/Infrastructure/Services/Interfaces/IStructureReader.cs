using Codebase.Structures.Common;

namespace Codebase.Structures.Infrastructure.Services.Interfaces
{
    public interface IStructureReader
    {
        public StructureDto Read(string structureName);
    }
}
