using System;
using System.Threading;

namespace MouseNet.Tools.Timers
{
    /// <summary>
    /// Represents a timer that can invoke methods through a <see cref="SynchronizationContext"/>.
    /// </summary>
    public class SynchronizedTimer : IDisposable
    {
        private readonly SynchronizationContext _sync;
        private readonly Timer _timer;
        private int _interval;

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizedTimer"/> class.
        /// </summary>
        /// <param name="sync">The <see cref="SynchronizationContext"/>.</param>
        public SynchronizedTimer
            (SynchronizationContext sync)
            {
            _sync = sync;
            _timer = new Timer(OnElapsed);
            }


        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="SynchronizedTimer" />
        ///     will stop after raising its <c>Elapsed</c> event, or reset and run again.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the timer will repeat; otherwise, <c>false</c>.
        /// </value>
        public bool Repeat { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="SynchronizedTimer" /> is enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled { get; set; }

        /// <summary>
        ///     Gets or sets the timer's interval in milliseconds.
        /// </summary>
        /// <value>
        ///     The interval.
        /// </value>
        public int Interval {
            get => _interval;
            set {
                _timer.Change(value, value);
                _interval = value;
            }
        }

        /// <summary>
        /// Occurs when the timer elapses.
        /// </summary>
        public event EventHandler Elapsed;

        /// <summary>
        /// Invokes the <c>Elapsed</c> event.
        /// </summary>
        /// <param name="state">The state.</param>
        public void OnElapsed
            (object state)
            {
            if (!Enabled) return;
            _sync.Send(o => Elapsed?.Invoke(this, EventArgs.Empty),
                       state);
            Enabled = Repeat;
            }

        /// <inheritdoc />
        public void Dispose()
            {
            _timer?.Dispose();
            }
    }
}