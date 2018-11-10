using System;
using System.Threading;

namespace MouseNet.Tools.Timers
{
    /// <inheritdoc cref="ITimer" />
    /// <summary>
    ///     Represents a timer that elapses at a specific time of day.
    /// </summary>
    /// <seealso cref="ITimer" />
    /// <seealso cref="System.IDisposable" />
    public class Alarm : ITimer, IDisposable
    {
        private readonly SynchronizedTimer _timer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Alarm" /> class.
        /// </summary>
        /// <param name="settings">The settings to configure the alarm with.</param>
        /// <param name="name">The name of the alarm.</param>
        public Alarm
            (IAlarmSettings settings,
             string name)
            {
            Name = name;
            AlarmTime = settings.AlarmTime;
            Repeat = settings.Repeat;
            _timer =
                new SynchronizedTimer(
                        SynchronizationContext.Current)
                        {
                        Interval = 60000
                        };
            _timer.Elapsed += OnElapsed;
            }

        /// <summary>
        ///     Gets the date on which this alarm last elapsed.
        /// </summary>
        /// <value>
        ///     The date on which the <c>Elapsed</c> event was last invoked.
        /// </value>
        public DateTime LastElapsed { get; private set; }
        /// <summary>
        ///     Gets time of day when the alarm elapses.
        /// </summary>
        /// <value>
        ///     The time of day when the alarm elapses.
        /// </value>
        public TimeSpan AlarmTime { get; }
        /// <summary>
        ///     Gets a value indicating whether this <see cref="Alarm" /> will repeat.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the alarm will repeat each day; otherwise, <c>false</c>.
        /// </value>
        public bool Repeat { get; }

        /// <inheritdoc />
        public void Dispose()
            {
            _timer?.Dispose();
            }

        /// <inheritdoc />
        public object Value => this;
        /// <inheritdoc />
        public Type Type => GetType();
        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public bool Enabled {
            get => _timer.Enabled;
            set => _timer.Enabled = value;
        }

        /// <inheritdoc />
        public event EventHandler<ElapsedEventArgs> Elapsed;

        /// <summary>
        ///     Called when the timer elapses. Invokes the <c>Elapsed</c> event if
        ///     the alarm has not elapsed yet today and it is the appropriate time.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">
        ///     The <see cref="EventArgs" /> instance containing
        ///     the event data.
        /// </param>
        protected virtual void OnElapsed
            (object sender,
             EventArgs args)
            {
            if (DateTime.Now.TimeOfDay < AlarmTime
             || LastElapsed == DateTime.Today) return;
            LastElapsed = DateTime.Today;
            Elapsed?.Invoke(this, new ElapsedEventArgs(Name));
            }
    }
}