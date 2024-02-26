using System;
using System.Collections.Generic;

namespace Codebase.Core.Common.Application.Exceptions
{
    public class LoadingStructuresException : Exception
    {
        public LoadingStructuresException(IEnumerable<string> structureNames)
        {
            Message = $"Failed to load structures (either not found or deserialized as null) : {string.Join(", ", structureNames)}";
        }

        public override string Message { get; }
    }
}
