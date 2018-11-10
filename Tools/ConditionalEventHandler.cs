using System;

namespace MouseNet.Tools
{
    /// <summary>
    ///     Wraps an event handler that can only be invoked when a
    ///     specified condition is true.
    /// </summary>
    /// <typeparam name="TEventArg">The type of the event argument.</typeparam>
    public class ConditionalEventHandler<TEventArg>
        where TEventArg : EventArgs
    {
        /// <summary>
        ///     Delegate representing a method which must return
        ///     true in order for the event handler to be invoked.
        /// </summary>
        /// <param name="handlerInstance">
        ///     The calling instance
        ///     of <c>ConditionalEventHandler</c>
        /// </param>
        /// <param name="arg">The argument received from the event.</param>
        /// <returns>
        ///     True if the handler should be invoked, or false
        ///     if it should not.
        /// </returns>
        public delegate bool HandlerCondition
            (ConditionalEventHandler<TEventArg> handlerInstance,
             TEventArg arg);

        private readonly HandlerCondition _condition;
        private readonly EventHandler<TEventArg> _handler;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConditionalEventHandler{TEventArg}" /> class.
        /// </summary>
        /// <param name="handler">The event handler.</param>
        /// <param name="handlerCondition">
        ///     The delegate that will be called to determine if
        ///     the event handler should be invoked.
        /// </param>
        public ConditionalEventHandler
            (EventHandler<TEventArg> handler,
             HandlerCondition handlerCondition)
            {
            _handler = handler;
            _condition = handlerCondition;
            }

        /// <summary>
        ///     Invokes the event handler contained in this instance if the given condition
        ///     returns true.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The arguments passed from the event.</param>
        /// <remarks>Connect this method to an event using the <c>+=</c> syntax.</remarks>
        public void Invoke
            (object sender,
             TEventArg args)
            {
            if (_condition(this, args)) _handler(sender, args);
            }
    }

    /// <summary>
    ///     A <c>ConditionalEventHandler</c> that takes an <see cref="INamedObject" /> as its argument,
    ///     and invokes its handler if its name is equal to the argument's name.
    /// </summary>
    /// <typeparam name="TArg">The type of the argument.</typeparam>
    /// <seealso cref="MouseNet.Tools.ConditionalEventHandler{TArg}" />
    /// <seealso cref="MouseNet.Tools.INamedObject" />
    /// <remarks>
    ///     This class also implements the <see cref="INamedObject" /> interface to expose
    ///     its <c>Name</c> property.
    /// </remarks>
    /// <inheritdoc cref="ConditionalEventHandler{TEventArg}" />
    /// <inheritdoc cref="INamedObject" />
    /// <seealso cref="ConditionalEventHandler{TEventArg}" />
    /// <seealso cref="INamedObject" />
    public class NamedObjectEventHandler<TArg>
        : ConditionalEventHandler<TArg>, INamedObject
        where TArg : EventArgs, INamedObject
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:MouseNet.Tools.NamedObjectEventHandler`1" /> class.
        /// </summary>
        /// <param name="handler">The event handler.</param>
        /// <param name="name">The name.</param>
        public NamedObjectEventHandler
            (EventHandler<TArg> handler,
             string name)
            : base(handler, NameMatches)
            {
            Name = name;
            }

        /// <inheritdoc />
        public object Value => this;
        /// <inheritdoc />
        public Type Type => GetType();
        /// <inheritdoc />
        public string Name { get; }

        /// <summary>
        ///     Checks if the argument's name is equal to the calling
        ///     <c>NamedObjectEventHandler</c>'s name.
        /// </summary>
        /// <param name="instance">The calling instance.</param>
        /// <param name="arg">The event argument.</param>
        /// <returns>True if the names match, false if they do not.</returns>
        /// <remarks>
        ///     This method is used as the
        ///     <see cref="ConditionalEventHandler{TEventArg}.HandlerCondition" /> for
        ///     instances of the <c>NamedObjectEventHandler</c> class.
        /// </remarks>
        private static bool NameMatches
            (ConditionalEventHandler<TArg> instance,
             TArg arg)
            {
            return arg.Name
                == ((NamedObjectEventHandler<TArg>) instance).Name;
            }
    }
}