using System;

namespace MouseNet.Tools.Timers
{
    
    /// <inheritdoc />
    /// <summary>
    ///     Exposes basic functions of a timer.
    /// </summary>
    /// <seealso cref="T:MouseNet.Tools.INamedObject" />
    public interface ITimer : INamedObject
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this timer is enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        bool Enabled { get; set; }
        /// <summary>
        ///     Occurs when the timer's interval is reached.
        /// </summary>
        event EventHandler<ElapsedEventArgs> Elapsed;
    }
}