using System.Collections.Generic;
using System.Diagnostics;

namespace ApplicationCode.Core.Common.General.Utils
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
            if (_stopwatches.ContainsKey(name))
                _stopwatches[name].Stop();

            MaloyAlert.Message("Stop stopwatch: " + name);
        }

        public static void End(string name)
        {
            MaloyAlert.Message("End stopwatch: " + name);
            
            if (_stopwatches.ContainsKey(name) == false)
                return;

            Stopwatch stopwatch = _stopwatches[name];
            _stopwatches.Remove(name);

            MaloyAlert.DebugMessage($"{name} took {stopwatch.Elapsed.TotalMilliseconds} ms");
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