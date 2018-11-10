using System;
using System.Threading;
using ElapsedHandler = MouseNet.Tools.NamedObjectEventHandler<MouseNet.Tools.Timers.ElapsedEventArgs>;
using TickHandler = MouseNet.Tools.NamedObjectEventHandler<MouseNet.Tools.Timers.TickEventArgs>;

namespace MouseNet.Tools.Timers
{
    public class TimerManager
    {
        private readonly NamedObjectList<Alarm> _alarms;
        private readonly Counter _counter;

        private readonly NamedObjectList<ElapsedHandler>
            _elapsedHandlers;

        private readonly NamedObjectList<TickHandler> _tickHandlers;

        public TimerManager()
            {
            _alarms = new NamedObjectList<Alarm>();
            _counter = new Counter(SynchronizationContext.Current);
            _elapsedHandlers = new NamedObjectList<ElapsedHandler>();
            _tickHandlers = new NamedObjectList<TickHandler>();
            }

        public ITimer AddAlarm
            (IAlarmSettings settings,
             string name)
            {
            _alarms.Remove(name);
            var result = new Alarm(settings, name);
            _alarms.Add(result);
            return result;
            }

        public void AddTimer
            (ICounterSettings instance,
             string name,
             EventHandler<ElapsedEventArgs> elapsedHandler,
             EventHandler<TickEventArgs> tickHandler)
            {
            _counter.AddAlarm(instance, name);
            SetElapsedHandler(name, elapsedHandler);
            SetTickHandler(name, tickHandler);
            }

        private void RemoveElapsedHandler
            (string name)
            {
            var handler = _elapsedHandlers.Find(name);
            if (handler.Name != name) return;
            _counter.Elapsed -= handler.Invoke;
            _elapsedHandlers.Remove(handler);
            }

        private void RemoveTickHandler
            (string name)
            {
            var handler = _tickHandlers.Find(name);
            if (handler.Name != name) return;
            _counter.Tick -= handler.Invoke;
            _tickHandlers.Remove(handler);
            }

        public void SetElapsedHandler
            (string name,
             EventHandler<ElapsedEventArgs> elapsedHandler)
            {
            RemoveElapsedHandler(name);
            var handler = new ElapsedHandler(elapsedHandler, name);
            _elapsedHandlers.Add(handler);
            _counter.Elapsed += handler.Invoke;
            }

        public void SetTickHandler
            (string name,
             EventHandler<TickEventArgs> tickHandler)
            {
            RemoveTickHandler(name);
            var handler = new TickHandler(tickHandler, name);
            _tickHandlers.Add(handler);
            _counter.Tick += handler.Invoke;
            }
    }
}