using System;

namespace MouseNet.Tools
{
    /// <summary>
    ///     Exposes a named object, which contains a
    ///     reference to an object and an associated name.
    /// </summary>
    public interface INamedObject
    {
        /// <summary>
        /// Gets the contained value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        object Value { get; }

        /// <summary>
        ///     Gets the type of the contained object.
        /// </summary>
        /// <value>
        ///     The type of the contained object.
        /// </value>
        Type Type { get; }

        /// <summary>
        ///     Gets the name associated with the contained object.
        /// </summary>
        /// <value>
        ///     The name of the contained object.
        /// </value>
        string Name { get; }
    }

    /// <inheritdoc />
    /// <typeparam name="TValue">The type of the object.</typeparam>
    public interface INamedObject<out TValue> : INamedObject
    {
        /// <summary>
        ///     Gets the contained <typeparamref name="TValue"/>.
        /// </summary>
        /// <value>
        ///     The contained object of type <typeparamref name="TValue"/>.
        /// </value>
        new TValue Value { get; }
    }
}