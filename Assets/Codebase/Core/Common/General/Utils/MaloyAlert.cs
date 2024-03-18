#define UNITY
//#define CONSOLE

namespace Codebase.Core.Common.General.Utils
{
    public static class MaloyAlert
    {
        // ReSharper disable once ConvertToConstant.Local
        // ReSharper disable once InconsistentNaming
        private static readonly bool _enabled = true;

        // ReSharper disable once ConvertToConstant.Local
        // ReSharper disable once InconsistentNaming
        private static readonly bool _blameEnabled = true;

        public static void Todo(object message)
        {
            Message($"TODO: {message}");
        }

        public static void Blame(object message)
        {
            if (_blameEnabled)
                Warning($"\"Horrible piece of shit\" {message}");
        }

        public static void DebugMessage(object message)
        {
            if (MaloyDebug.IsDebugging)
                Message(message);
        }

        public static void DebugWarning(object message)
        {
            if (MaloyDebug.IsDebugging)
                Warning(message);
        }

        #region Unity

#if UNITY
        public static void Message(object message)
        {
            if (_enabled == false)
                return;

            UnityEngine.Debug.Log(message);
        }

        public static void Warning(object message)
        {
            if (_enabled == false)
                return;

            UnityEngine.Debug.LogWarning(message);
        }

        public static void Error(object message)
        {
            if (_enabled == false)
                return;

            UnityEngine.Debug.LogError(message);
        }

#endif

        #endregion

        #region Console

#if CONSOLE
        public static void Message(object message)
        {
            if (_enabled == false)
                return;

            Console.WriteLine(message);
        }

        public static void Warning(object message)
        {
            if (_enabled == false)
                return;

            WriteWithColor(message, ConsoleColor.Yellow);
        }

        public static void Error(object message)
        {
            if (_enabled == false)
                return;

            WriteWithColor(message, ConsoleColor.Red);
        }

        private static void WriteWithColor(object message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
#endif

        #endregion
    }
}
