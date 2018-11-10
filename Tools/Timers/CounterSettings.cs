using MouseNet.Tools.IO;

namespace MouseNet.Tools.Timers
{
    /// <inheritdoc cref="ICounterSettings" />
    public class CounterSettings
        : SerializableObject<ICounterSettings>, ICounterSettings
    {
        /// <inheritdoc />
        public int Interval { get; set; }
        /// <inheritdoc />
        public int Unit { get; set; }
        /// <inheritdoc />
        public bool Repeat { get; set; }
    }
}