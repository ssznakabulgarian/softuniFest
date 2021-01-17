using System;
using System.Threading;

namespace WebMonitoringApi.Common
{
    public class TimerManager
    {
        private Timer _timer;
        private AutoResetEvent _autoResetEvent;
        private Action _action;

        public TimerManager(Action action, int requestFrequencySeconds, int delayDurationSeconds)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, delayDurationSeconds, requestFrequencySeconds);
        }

        public void Execute(object stateInfo)
        {
            _action();
        }

        public void Suspend()
        {
            _timer.Dispose();
        }
    }
}

