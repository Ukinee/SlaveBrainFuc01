using ApplicationCode.Core.Common.General.Utils;

namespace Codebase.Core.Common.Application.Extensions
{
    public static class ObjectExtensions
    {
        public static void Show(this object message)
        {
            MaloyAlert.Message(message);
        }
        
        public static void Log(this object message)
        {
            MaloyAlert.Message(message);
        }
    }
}
