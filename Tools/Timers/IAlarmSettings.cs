using System;
using MouseNet.Tools.IO;

namespace MouseNet.Tools.Timers
{
    /// <inheritdoc />
    /// <summary>
    ///     Exposes settings to configure an <see cref="Alarm" />.
    /// </summary>
    /// <seealso cref="ISelfSerializable" />
    public interface IAlarmSettings : ISelfSerializable
    {
        /// <summary>
        ///     Gets or sets the alarm time.
        /// </summary>
        /// <value>
        ///     The time at which the alarm will elapse.
        /// </value>
        TimeSpan AlarmTime { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether the alarm will
        ///     repeat after it elapses.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the alarm will repeat; otherwise, <c>false</c>.
        /// </value>
        bool Repeat { get; set; }
    }
}