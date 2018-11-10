using System.IO;

namespace MouseNet.Tools.IO
{
    /// <summary>
    ///     Base class representing a forward linked list of values
    ///     that can be written to a file.
    /// </summary>
    public class FileLinkedListBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FileLinkedListBase" /> class.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="root">The root node.</param>
        protected FileLinkedListBase
            (string path,
             IFileNode root)
            {
            FilePath = path;
            Root = root;
            }

        /// <summary>
        ///     Gets the root node of the list.
        /// </summary>
        /// <value>
        ///     The root node.
        /// </value>
        protected IFileNode Root { get; }

        /// <summary>
        ///     Gets or sets the file path.
        /// </summary>
        /// <value>
        ///     The file path.
        /// </value>
        public string FilePath {
            get => File.FullName;
            set => File = new FileInfo(Path.GetFullPath(value));
        }

        /// <summary>
        ///     Gets the file used by the list.
        /// </summary>
        /// <value>
        ///     The file.
        /// </value>
        protected FileInfo File { get; private set; }

        /// <summary>
        ///     Writes all nodes in the list to the file.
        /// </summary>
        public void WriteAll()
            {
            using (var stream = File.Create())
                {
                Root.RecursiveSaveTo(stream);
                }
            }

        /// <summary>
        ///     Reads all values from the file and stores them in
        ///     their corresponding nodes.
        /// </summary>
        public void ReadAll()
            {
            if (!File.Exists) return;
            using (var stream = File.OpenRead())
                {
                Root.RecursiveLoadFrom(stream);
                }
            }

        /// <summary>
        /// Internal method for inserting a node after another node.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="newNode">The new node.</param>
        protected static void InsertInternal
            (IFileNode parentNode,
             IFileNode newNode)
            {
            newNode.Next = parentNode.Next;
            parentNode.Next = newNode;
            }

        /// <summary>
        ///     Writes the nodes below the specified depth to the file.
        /// </summary>
        /// <param name="depth">The depth.</param>
        protected void Write
            (int depth)
            {
            using (var stream = File.OpenWrite())
                {
                var node = Root[depth];
                stream.Seek(node.Location, SeekOrigin.Begin);
                node.RecursiveSaveTo(stream);
                stream.SetLength(stream.Position);
                }
            }

        /// <summary>
        ///     Writes the node with the specified name and all of its
        ///     children to the file.
        /// </summary>
        /// <param name="name">The name.</param>
        protected void Write
            (string name)
            {
            using (var stream = File.OpenWrite())
                {
                var node = Root.Find(name);
                stream.Seek(node.Location, SeekOrigin.Begin);
                node.RecursiveSaveTo(stream);
                stream.SetLength(stream.Position);
                }
            }
    }
}