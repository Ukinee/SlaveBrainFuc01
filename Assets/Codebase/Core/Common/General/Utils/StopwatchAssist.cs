using System.Collections.Generic;
using System.Diagnostics;

namespace Codebase.Core.Common.General.Utils
{
    public static class StopwatchAssist
    {
        private static readonly Dictionary<string, Stopwatch> _stopwatches =
            new Dictionary<string, Stopwatch>();

        public static void Start(string name)
        {
            MaloyAlert.Message("Start stopwatch: " + name);

            if (_stopwatches.ContainsKey(name) == false)
                _stopwatches[name] = new Stopwatch();

            _stopwatches[name].Start();
        }

        public static void Stop(string name)
        {
            if (_stopwatches.TryGetValue(name, out Stopwatch stopwatch))
            {
                stopwatch.Stop();
                MaloyAlert.Message("Stop stopwatch: " + name);
            }
        }

        public static void End(string name, int iterations = 1)
        {
            if (_stopwatches.ContainsKey(name) == false)
                return;

            Stopwatch stopwatch = _stopwatches[name];
            stopwatch.Stop();
            _stopwatches.Remove(name);

            MaloyAlert.DebugMessage($"\"{name}\" took {stopwatch.Elapsed.TotalMilliseconds} total and {stopwatch.Elapsed.TotalMilliseconds / iterations} ms per iteration");
        }

        public static void Clear()
        {
            MaloyAlert.Message("Clear stopwatch");

            foreach (Stopwatch stopwatch in _stopwatches.Values)
                stopwatch.Reset();

            _stopwatches.Clear();
        }
    }
}
