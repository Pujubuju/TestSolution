using System;
using System.Collections.Generic;
using System.Diagnostics;
using CollectionsPerformanceComparison.Actions;
using CollectionsPerformanceComparison.Actions.Abstraction;

namespace CollectionsPerformanceComparison
{
    public class ActionTimeMeasurement<Tc, Ti> where Tc : ICollection<Ti>, new()
    {
        private readonly ExecutableAction<Tc, Ti> _action;

        public ActionTimeMeasurement(ExecutableAction<Tc, Ti> action)
        {
            _action = action;
        }

        public TimeSpan Measure()
        {
            _action.Prepare();
            var timer = new Stopwatch();
            timer.Start();
            _action.Execute();
            timer.Stop();
            return timer.Elapsed;
        }

    }
}
