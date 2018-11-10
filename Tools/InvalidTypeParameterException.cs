using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace MouseNet.Tools
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents an error that occurred due to an incorrect
    ///     generic type parameter.
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class InvalidTypeParameterException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="InvalidTypeParameterException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidTypeParameterException
            (string message)
            : base(message) { }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="InvalidTypeParameterException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The inner exception.</param>
        public InvalidTypeParameterException
            (string message,
             Exception inner)
            : base(message, inner) { }

        /// <inheritdoc />
        ///<summary>Deserialization constructor.</summary>
        protected InvalidTypeParameterException
            (SerializationInfo info,
             StreamingContext context)
            : base(info, context)
            {
            ProvidedType =
                Type.GetType(info.GetString("ProvidedType"));
            }

        /// <summary>
        ///     Gets or sets the provided type.
        /// </summary>
        /// <value>
        ///     The type provided as a generic type parameter which caused
        ///     the exception to be thrown.
        /// </value>
        public Type ProvidedType { get; set; }

        /// <inheritdoc />
        ///<summary>Serialization method.</summary>
        [SecurityPermission(SecurityAction.Demand,
            SerializationFormatter = true)]
        public override void GetObjectData
            (SerializationInfo info,
             StreamingContext context)
            {
            if (info == null)
                throw new ArgumentException(nameof(info));
            info.AddValue("ProvidedType", ProvidedType.FullName);
            base.GetObjectData(info, context);
            }
    }
}