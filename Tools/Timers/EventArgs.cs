using System;

namespace MouseNet.Tools.Timers
{
    /// <inheritdoc cref="INamedObject" />
    /// <summary>
    ///     Represents the data associated with a timer's <c>Elapsed</c> event.
    /// </summary>
    /// <seealso cref="EventArgs" />
    /// <seealso cref="INamedObject" />
    public class ElapsedEventArgs : EventArgs, INamedObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ElapsedEventArgs" /> class.
        /// </summary>
        /// <param name="name">The name of the elapsed timer.</param>
        public ElapsedEventArgs
            (string name)
            {
            Name = name;
            }

        /// <inheritdoc />
        public object Value => this;
        /// <inheritdoc />
        public Type Type => GetType();
        /// <inheritdoc />
        public string Name { get; }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Represents the data associated with a <see cref="Counter" />'s <c>Tick</c> event.
    /// </summary>
    /// <seealso cref="ElapsedEventArgs" />
    public class TickEventArgs : ElapsedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:MouseNet.Tools.Timers.TickEventArgs" /> class.
        /// </summary>
        /// <param name="timerName">Name of the timer.</param>
        /// <param name="timeRemaining">The time remaining.</param>
        public TickEventArgs
            (string timerName,
             TimeSpan timeRemaining)
            : base(timerName)
            {
            TimeRemaining = timeRemaining;
            }

        /// <summary>
        ///     Gets the alarm's time remaining at the time of the
        ///     <c>Tick</c>.
        /// </summary>
        /// <value>
        ///     The time remaining.
        /// </value>
        public TimeSpan TimeRemaining { get; }
    }
}