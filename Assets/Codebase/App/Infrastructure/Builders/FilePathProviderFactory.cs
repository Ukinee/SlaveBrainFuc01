using Codebase.Core.Common.Application.Utilities.Constants;
using Codebase.Core.Common.Application.Utils;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Codebase.App.Infrastructure.Builders
{
    public class FilePathProviderFactory
    {
        public FilePathProvider Load()
        {
            string json = Resources.Load<TextAsset>(PathConstants.Environment).text;
            
            return JsonConvert.DeserializeObject<FilePathProvider>(json);
        }
    }
}