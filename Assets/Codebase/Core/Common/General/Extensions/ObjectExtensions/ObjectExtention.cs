using Codebase.Core.Common.General.Utils;

namespace Codebase.Core.Common.General.Extensions.ObjectExtensions
{
    public static class ObjectExtension
    {
        public static void Log(this object obj)
        {
            MaloyAlert.Message(obj.ToString());
        }
        
        public static void Show(this object obj)
        {
            MaloyAlert.Message(obj.ToString());
        }
    }
}
