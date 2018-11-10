using System.IO;

namespace MouseNet.Tools.IO
{
    /// <summary>
    /// Exposes a node of a file list.
    /// </summary>
    public interface IFileNode
    {
        /// <summary>
        ///     Gets the <see cref="IFileNode" /> at the specified depth.
        /// </summary>
        /// <value>
        ///     The <see cref="IFileNode" /> at depth <c>i</c>.
        /// </value>
        /// <param name="i">The depth of the node to retreive.</param>
        /// <returns></returns>
        IFileNode this
            [int i] { get; }

        /// <summary>
        ///     Gets or sets the next node in the list.
        /// </summary>
        /// <value>
        ///     The node's direct child node.
        /// </value>
        IFileNode Next { get; set; }
        /// <summary>
        ///     Gets the depth of the list at this node. This number
        ///     represents how many nodes are beneath this node.
        /// </summary>
        /// <value>
        ///     The depth.
        /// </value>
        int Depth { get; }
        /// <summary>
        ///     Gets the location of this node in the file.
        /// </summary>
        /// <value>
        ///     The location in the stream where this node can be found.
        /// </value>
        long Location { get; }
        /// <summary>
        ///     Gets the length of this node in the file.
        /// </summary>
        /// <value>
        ///     The length of this node.
        /// </value>
        long Length { get; }

        /// <summary>
        ///     Serializes the node to the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        void SaveTo
            (Stream stream);
        

        /// <summary>
        ///     Serializes the node and each of its children to the
        ///     specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        void RecursiveSaveTo
            (Stream stream);

        /// <summary>
        ///     Deserializes the node's contents from the specified stream
        ///     using its location.
        /// </summary>
        /// <param name="stream">The stream.</param>
        void LoadFrom
            (Stream stream);

        /// <summary>
        ///     Deserializes the node and all of its children from the specified
        ///     stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        void RecursiveLoadFrom
            (Stream stream);

        /// <summary>
        ///     Finds the node with the specified name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>
        ///     The node with the specified name, or null if it does
        ///     not exist.
        /// </returns>
        IFileNode Find
            (string name);

        /// <summary>
        ///     Adds a child node to this node.
        /// </summary>
        /// <typeparam name="T">The type of the object's value.</typeparam>
        /// <param name="obj">The contents of the node.</param>
        /// <returns>
        ///     The created node. This method can be chained to create
        ///     longer lists.
        /// </returns>
        IFileNode WithChild<T>
            (INamedObject<T> obj)
            where T : ISelfSerializable, new();
    }
}