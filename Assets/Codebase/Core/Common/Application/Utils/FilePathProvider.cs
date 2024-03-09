using System;
using System.Collections.Generic;

namespace Codebase.Core.Common.Application.Utils
{
    [Serializable]
    public class FilePathProvider
    {
        public FileData General = new FileData();
        public FileData Structures = new FileData();
        public FileData Game = new FileData();
        public FileData Forms = new FileData();
    }

    public class FileData
    {
        public Dictionary<string, string> Data = new Dictionary<string, string>();
    }
}