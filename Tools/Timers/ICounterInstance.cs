using System;

namespace MouseNet.Tools.Timers
{

    /// <summary>
    ///     Exposes configuration values for a <see cref="Counter" /> alarm.
    /// </summary>
    /// <seealso cref="INamedObject" />
    public interface ICounterInstance : INamedObject
    {
        /// <summary>
        ///     Gets a value indicating whether this <see cref="ICounterInstance" /> is running.
        /// </summary>
        /// <value>
        ///     <c>true</c> if running; otherwise, <c>false</c>.
        /// </value>
        bool Running { get; set; }
        /// <summary>
        ///     Gets the alarm's interval.
        /// </summary>
        /// <value>
        ///     The interval.
        /// </value>
        TimeSpan Interval { get; }
        /// <summary>
        ///     Gets the time remaining before the alarm elapses.
        /// </summary>
        /// <value>
        ///     The time remaining.
        /// </value>
        TimeSpan TimeRemaining { get; }
    }
}