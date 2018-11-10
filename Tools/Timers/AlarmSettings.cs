using System;
using MouseNet.Tools.IO;

namespace MouseNet.Tools.Timers
{
    /// <inheritdoc cref="IAlarmSettings" />
    public class AlarmSettings
        : SerializableObject<IAlarmSettings>, IAlarmSettings
    {
        /// <inheritdoc />
        public TimeSpan AlarmTime { get; set; }
        /// <inheritdoc />
        public bool Repeat { get; set; }
    }
}