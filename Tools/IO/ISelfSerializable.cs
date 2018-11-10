using System.IO;

namespace MouseNet.Tools.IO
{
    /// <summary>
    ///     Exposes a self-serlializable object implementing
    ///     methods to serialize and deserialize itself.
    /// </summary>
    public interface ISelfSerializable
    {
        /// <summary>
        ///     Serializes the object to the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        void Serialize
            (Stream stream);

        /// <summary>
        ///     Deserializes the object from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <remarks>
        ///     When implementing this method, populate
        ///     the object's fields and properties from the
        ///     stream rather than creating a new instance.
        /// </remarks>
        void Deserialize
            (Stream stream);
    }
}