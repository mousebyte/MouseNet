using System;

namespace MouseNet.Tools
{
    /// <summary>
    ///     Represents a named object, which contains an object instance and an associated name.
    /// </summary>
    /// <seealso cref="MouseNet.Tools.INamedObject" />
    public struct NamedObject : INamedObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NamedObject" /> struct.
        ///     Uses the object's type name as the value of the <c>Name</c> property.
        /// </summary>
        /// <param name="obj">The object.</param>
        public NamedObject
            (object obj)
            {
            Value = obj;
            Type = obj.GetType();
            Name = Type.Name;
            }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NamedObject" /> struct.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="obj">The object.</param>
        public NamedObject
            (string name,
             object obj)
            {
            Value = obj;
            Type = obj.GetType();
            Name = name;
            }

        /// <inheritdoc />
        public object Value { get; }
        /// <inheritdoc />
        public Type Type { get; }
        /// <inheritdoc />
        public string Name { get; }
    }

    /// <inheritdoc cref="INamedObject{TValue}" />
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <seealso cref="INamedObject" />
    public struct NamedObject<TObject> : INamedObject<TObject>
        where TObject : class
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NamedObject{TObject}" /> struct.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <param name="obj">The object.</param>
        public NamedObject
            (string name,
             TObject obj)
            {
            Value = obj;
            Type = obj.GetType();
            Name = name;
            }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NamedObject{TObject}" /> struct.
        ///     Uses the object's type name as the value of the <c>Name</c> property.
        /// </summary>
        /// <param name="obj">The object.</param>
        public NamedObject
            (TObject obj)
            {
            Value = obj;
            Type = obj.GetType();
            Name = Type.Name;
            }

        /// <summary>
        ///     Performs an explicit conversion from <typeparamref name="TObject" /> to
        ///     <see cref="NamedObject{TObject}" />. The type name will be
        ///     used as the value of the <c>Name</c> property.
        /// </summary>
        /// <param name="obj">This object.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator NamedObject<TObject>
            (TObject obj)
            {
            return new NamedObject<TObject>(obj);
            }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="NamedObject{TObject}" />
        ///     to <typeparamref name="TObject" />. Effectively unpacks the contained object.
        /// </summary>
        /// <param name="obj">This object.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator TObject
            (NamedObject<TObject> obj)
            {
            return obj.Value;
            }

        /// <inheritdoc />
        object INamedObject.Value => Value;
        /// <inheritdoc />
        public TObject Value { get; }
        /// <inheritdoc />
        public Type Type { get; }
        /// <inheritdoc />
        public string Name { get; }
    }
}