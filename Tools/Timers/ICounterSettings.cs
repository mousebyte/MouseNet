using MouseNet.Tools.IO;

namespace MouseNet.Tools.Timers
{
    /// <inheritdoc />
    /// <summary>
    ///     Exposes settings to configure a <see cref="Counter" />.
    /// </summary>
    /// <seealso cref="ISelfSerializable" />
    public interface ICounterSettings : ISelfSerializable
    {
        /// <summary>
        ///     Gets or sets the interval.
        /// </summary>
        /// <value>
        ///     The interval at which the timer will elapse.
        /// </value>
        int Interval { get; set; }
        /// <summary>
        ///     Gets or sets the unit of time used for the interval.
        /// </summary>
        /// <value>
        ///     The unit.
        /// </value>
        int Unit { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether the timer
        ///     will repeat after it elapses.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the timer will repeat; otherwise, <c>false</c>.
        /// </value>
        bool Repeat { get; set; }
    }
}