using System;
using System.Threading;

namespace MouseNet.Tools.Timers
{
    /// <inheritdoc cref="ITimer" />
    /// <summary>
    ///     Represents a collection of alarms that elapse after a specified interval.
    /// </summary>
    /// <seealso cref="T:System.IDisposable" />
    /// <seealso cref="T:MouseNet.Tools.Timers.ITimer" />
    public class Counter : IDisposable, ITimer
    {
        private static readonly TimeSpan OneSecond =
            TimeSpan.FromSeconds(1);

        private readonly NamedObjectList<CounterInstance> _alarms;
        private readonly SynchronizedTimer _timer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Counter" /> class.
        /// </summary>
        /// <param name="context">
        ///     The synchronization context to use when creating
        ///     the timer.
        /// </param>
        public Counter
            (SynchronizationContext context)
            {
            _alarms = new NamedObjectList<CounterInstance>();
            _timer = new SynchronizedTimer(context)
                {
                Interval = (int) OneSecond.TotalMilliseconds
                };
            _timer.Elapsed += OnTick;
            }

        /// <summary>
        ///     Gets the <see cref="ICounterInstance" /> at the specified index.
        /// </summary>
        /// <value>
        ///     The <see cref="ICounterInstance" />.
        /// </value>
        /// <param name="i">The index.</param>
        /// <returns>The configuration object at index <c>i</c>.</returns>
        public ICounterInstance this
            [int i] =>
            _alarms[i];

        /// <inheritdoc />
        public void Dispose()
            {
            _timer?.Dispose();
            }

        /// <inheritdoc />
        public bool Enabled {
            get => _timer.Enabled;
            set => _timer.Enabled = value;
        }

        /// <inheritdoc />
        public event EventHandler<ElapsedEventArgs> Elapsed;
        /// <inheritdoc />
        public object Value => this;
        /// <inheritdoc />
        public Type Type => GetType();
        /// <inheritdoc />
        public string Name => Type.Name;

        /// <summary>
        ///     Adds a new alarm to the counter.
        /// </summary>
        /// <param name="instance">The settings for the new alarm.</param>
        /// <param name="name">The name of the alarm.</param>
        /// <exception cref="ArgumentException">
        ///     Provided settings object does not represent
        ///     a timer.
        /// </exception>
        /// <returns>The created counter instance.</returns>
        public ICounterInstance AddAlarm
            (ICounterSettings instance,
             string name)
            {
            var alarm = new CounterInstance(instance, name);
            _alarms.Add(alarm);
            return alarm;
            }

        /// <summary>
        ///     Removes the alarm with the given name from the counter.
        /// </summary>
        /// <param name="name">The name of the alarm to remove.</param>
        public void RemoveAlarm
            (string name)
            {
            _alarms.Remove(name);
            }

        /// <summary>
        ///     Gets the alarm with the given name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>The configuration object with the given name.</returns>
        public ICounterInstance GetAlarm
            (string name)
            {
            return _alarms.Find(name);
            }

        /// <summary>
        ///     Occurs once for each running alarm when one second elapses.
        /// </summary>
        public event EventHandler<TickEventArgs> Tick;

        /// <summary>
        ///     Called when one second elapses. Invokes the <c>Tick</c> event once for
        ///     each running alarm, passing a <see cref="TimeSpan" /> representing the
        ///     amount of time left until the alarm elapses.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">
        ///     The <see cref="EventArgs" /> instance containing the event
        ///     data.
        /// </param>
        private void OnTick
            (object sender,
             EventArgs args)
            {
            foreach (var alarm in _alarms)
                {
                if (!alarm.Running) continue;
                alarm.Tick();
                if (alarm.TimeRemaining == TimeSpan.Zero)
                    OnElapsed(alarm);
                else
                    Tick?.Invoke(this,
                                 new TickEventArgs(
                                     alarm.Name,
                                     alarm.TimeRemaining));
                }
            }

        /// <summary>
        ///     Called when one of the counter's alarms elapses.
        /// </summary>
        /// <param name="alarm">The alarm.</param>
        private void OnElapsed
            (CounterInstance alarm)
            {
            Elapsed?.Invoke(this, new ElapsedEventArgs(alarm.Name));
            alarm.Running = alarm.Repeat;
            alarm.Reset();
            }

        /// <inheritdoc />
        /// <seealso cref="ICounterInstance" />
        private class CounterInstance : ICounterInstance
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="CounterInstance" /> class.
            /// </summary>
            /// <param name="instance">The settings to derive the configuration values from.</param>
            /// <param name="name">The name of the alarm.</param>
            public CounterInstance
                (ICounterSettings instance,
                 string name)
                {
                Repeat = instance.Repeat;
                Name = name;
                switch (instance.Unit)
                    {
                    case 0:
                        Interval =
                            TimeSpan.FromSeconds(instance.Interval);
                        break;
                    case 1:
                        Interval =
                            TimeSpan.FromMinutes(instance.Interval);
                        break;
                    case 2:
                        Interval =
                            TimeSpan.FromHours(instance.Interval);
                        break;
                    default:
                        Interval = TimeSpan.Zero;
                        break;
                    }

                Reset();
                }

            /// <summary>
            ///     Gets a value indicating whether this <see cref="CounterInstance" />
            ///     will repeat after it elapses.
            /// </summary>
            /// <value>
            ///     <c>true</c> if the counter will repeat; otherwise, <c>false</c>.
            /// </value>
            public bool Repeat { get; }
            /// <inheritdoc />
            public bool Running { get; set; }
            /// <inheritdoc />
            public object Value => this;
            /// <inheritdoc />
            public Type Type => GetType();
            /// <inheritdoc />
            public string Name { get; }
            /// <inheritdoc />
            public TimeSpan Interval { get; }
            /// <inheritdoc />
            public TimeSpan TimeRemaining { get; private set; }

            /// <summary>
            ///     Resets the time remaining.
            /// </summary>
            public void Reset()
                {
                TimeRemaining = Interval;
                }

            /// <summary>
            ///     Decriments the time remaining.
            /// </summary>
            public void Tick()
                {
                TimeRemaining -= OneSecond;
                }
        }
    }
}