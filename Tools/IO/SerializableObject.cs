using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace MouseNet.Tools.IO
{
    //TODO: Major changes here, update doc comments
    

    /// <summary>
    ///     Represents an object that can serialize and deserialize
    ///     itself.
    /// </summary>
    /// <typeparam name="T">
    ///     The type which exposes the properties to be used in the
    ///     serialization process.
    /// </typeparam>
    /// <seealso cref="MouseNet.Tools.IO.ISelfSerializable" />
    /// <inheritdoc />
    /// <seealso cref="ISelfSerializable" />
    public class SerializableObject<T> : ISelfSerializable
    {
        private readonly object _object;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializableObject{T}"/> class.
        /// </summary>
        /// <param name="obj">The object to be serialized in place of <c>this</c>.</param>
        protected SerializableObject
            (T obj)
            {
            _object = obj;
            }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SerializableObject{T}" />
        ///     class.
        /// </summary>
        /// <exception cref="InvalidTypeParameterException">
        ///     Provided type is not an interface,
        ///     or
        ///     provided interface is not implemented by this type.
        /// </exception>
        protected SerializableObject()
            {
            if (!typeof(T).IsInterface)
                throw new InvalidTypeParameterException(
                    "Provided type is not an interface.");
            if (!GetType().GetInterfaces().Contains(typeof(T)))
                throw new InvalidTypeParameterException(
                    "Provided interface is not implemented by this type.");
            _object = this;
            }

        /// <inheritdoc />
        /// <summary>
        ///     Serializes the object to the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <remarks>
        ///     A <see cref="System.Type" /> object representing this object
        ///     is serialized to the stream first, followed by the properties
        ///     exposed by <c>T</c>.
        /// </remarks>
        public virtual void Serialize
            (Stream stream)
            {
            var fmt = new BinaryFormatter();
            fmt.Serialize(stream, _object.GetType());
            foreach (var prop in typeof(T).GetProperties())
                fmt.Serialize(stream, prop.GetValue(_object, null));
            }

        /// <inheritdoc />
        /// <summary>
        ///     Deserializes the object from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <exception cref="System.InvalidOperationException">
        ///     The <see cref="System.Type" /> object deserialized from the stream
        ///     is not equal to this object's type.
        /// </exception>
        public virtual void Deserialize
            (Stream stream)
            {
            var fmt = new BinaryFormatter();
            if ((Type) fmt.Deserialize(stream) != _object.GetType())
                throw new InvalidOperationException(
                    "Incorrect type found in stream.");
            foreach (var prop in typeof(T).GetProperties())
                prop.SetValue(_object, fmt.Deserialize(stream), null);
            }
    }

    /// <summary>
    ///     Represents a self-serializable <see cref="INamedObject{TValue}" />.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TInterface">The type of the interface.</typeparam>
    /// <inheritdoc cref="SerializableObject{T}" />
    /// <seealso cref="MouseNet.Tools.IO.SerializableObject{TInterface}" />
    /// <seealso cref="INamedObject{TValue}" />
    public class NamedSerializable<TValue, TInterface>
        : SerializableObject<TInterface>, INamedObject<TValue>
        where TValue : TInterface
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedSerializable{TValue, TInterface}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="name">The name.</param>
        public NamedSerializable
            (TValue value,
             string name)
            : base(value)
            {
            Value = value;
            Type = typeof(TValue);
            Name = name;
            }

        /// <inheritdoc />
        object INamedObject.Value => Value;
        /// <inheritdoc />
        public TValue Value { get; }
        /// <inheritdoc />
        public Type Type { get; }
        /// <inheritdoc />
        public string Name { get; set; }
    }
}