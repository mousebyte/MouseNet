using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MouseNet.Tools.IO
{
    /// <summary>
    ///     Represents a forward linked list of objects that can be written to a file.
    /// </summary>
    /// <seealso cref="MouseNet.Tools.IO.FileLinkedListBase" />
    public class FileLinkedList : FileLinkedListBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FileLinkedList" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FileLinkedList
            (string path)
            : base(path, new RootFileNode()) { }

        /// <summary>
        ///     Inserts a new node with the given object after the node at
        ///     the specified depth.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="obj">The contents of the node.</param>
        /// <param name="depth">The depth of the node's parent.</param>
        /// <returns>The created file node.</returns>
        public IFileNode Insert<TValue>
            (INamedObject<TValue> obj,
             int depth)
            where TValue : ISelfSerializable, new()
            {
            var result = FileNode.Create(obj);
            InsertInternal(Root[depth], result);
            return result;
            }

        /// <summary>
        ///     Appends a new node with the given object to the bottom of the
        ///     list.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="obj">The contents of the node.</param>
        /// <returns>The created file node.</returns>
        public IFileNode Append<TValue>
            (INamedObject<TValue> obj)
            where TValue : ISelfSerializable, new()
            {
            return Insert(obj, 0);
            }

        /// <inheritdoc />
        /// <summary>
        ///     Represents the root node of a <see cref="FileLinkedList" />.
        ///     Writes header information to the beginning of the file.
        /// </summary>
        /// <seealso cref="FileLinkedList.FileNode" />
        private class RootFileNode : FileNode
        {
            /// <inheritdoc />
            public RootFileNode()
                : base(null, "root") { }

            /// <summary>
            ///     Gets the size of the header.
            /// </summary>
            /// <value>
            ///     The size of the header.
            /// </value>
            private long Size => Depth * sizeof(long) + sizeof(int);

            /// <inheritdoc />
            public override void RecursiveSaveTo
                (Stream stream)
                {
                //reserve space at the top of the file
                stream.Seek(Size, SeekOrigin.Begin);
                //save all child nodes (which sets their location)
                Next.RecursiveSaveTo(stream);
                //then write the header data
                SaveTo(stream);
                }

            /// <inheritdoc />
            public override void SaveTo
                (Stream stream)
                {
                stream.Seek(0, SeekOrigin.Begin);
                //write the depth aka total number of child nodes
                stream.Write(BitConverter.GetBytes(Next.Depth),
                             0,
                             sizeof(int));
                //traverse the list and write each node's location
                var node = Next;
                while (node != null)
                    {
                    stream.Write(BitConverter.GetBytes(node.Location),
                                 0,
                                 sizeof(long));
                    node = node.Next;
                    }
                }

            /// <inheritdoc />
            public override void LoadFrom
                (Stream stream)
                {
                stream.Seek(0, SeekOrigin.Begin);
                var buff = new byte[sizeof(long)];
                //read the number of child nodes
                var count = stream.Read(buff, 0, sizeof(int));
                if (count < Depth)
                    throw new InvalidOperationException();
                for (var i = 0; i < count; i++)
                    {
                    stream.Read(buff, 0, sizeof(long));
                    ((FileNode) Next[i]).Location =
                        BitConverter.ToInt64(buff, 0);
                    }
                }
        }

        private class FileNode
            : IFileNode, INamedObject<ISelfSerializable>
        {
            protected FileNode
                (ISelfSerializable value,
                 string name)
                {
                Value = value;
                Name = name;
                Type = typeof(ISelfSerializable);
                Location = 0;
                Length = 0;
                }

            public IFileNode this
                [int i] =>
                Depth == i
                    ? this
                    : Next[i];

            public IFileNode Next { get; set; }
            public int Depth => Next?.Depth + 1 ?? 0;
            public long Location { get; protected internal set; }
            public long Length { get; private set; }

            /// <inheritdoc />
            public virtual void SaveTo
                (Stream stream)
                {
                SaveInternal(stream);
                if (stream.Position > Next.Location)
                    Next.SaveTo(stream);
                }

            /// <inheritdoc />
            public virtual void LoadFrom
                (Stream stream)
                {
                if (Location != 0)
                    stream.Seek(Location, SeekOrigin.Begin);
                var fmt = new BinaryFormatter();
                //skip name, as it has already been set in RootFileNode
                Name = (string) fmt.Deserialize(stream);
                Value.Deserialize(stream);
                }

            /// <inheritdoc />
            public virtual void RecursiveSaveTo
                (Stream stream)
                {
                SaveInternal(stream);
                Next?.RecursiveSaveTo(stream);
                }

            /// <inheritdoc />
            public virtual void RecursiveLoadFrom
                (Stream stream)
                {
                LoadFrom(stream);
                Next?.RecursiveLoadFrom(stream);
                }

            /// <inheritdoc />
            public IFileNode WithChild<T>
                (INamedObject<T> obj)
                where T : ISelfSerializable, new()
                {
                var result = Create(obj);
                Next = result;
                return result;
                }

            /// <inheritdoc />
            public IFileNode Find
                (string name)
                {
                return Name == name
                           ? this
                           : Next.Find(name);
                }

            /// <inheritdoc />
            object INamedObject.Value => Value;
            /// <inheritdoc />
            public ISelfSerializable Value { get; }
            /// <inheritdoc />
            public Type Type { get; }
            /// <inheritdoc />
            public string Name { get; private set; }

            ///<summary>Internal save method</summary>
            private void SaveInternal
                (Stream stream)
                {
                Location = stream.Position;
                var fmt = new BinaryFormatter();
                //serialize name then value
                fmt.Serialize(stream, Name);
                Value.Serialize(stream);
                Length = stream.Position - Location;
                }

            /// <summary>
            ///     Creates a new <see cref="FileNode" /> with the specified object.
            /// </summary>
            /// <typeparam name="T">The type of the object's value.</typeparam>
            /// <param name="obj">The contents of the node.</param>
            /// <returns></returns>
            public static FileNode Create<T>
                (INamedObject<T> obj)
                where T : ISelfSerializable, new()
                {
                return new FileNode(obj.Value, obj.Name);
                }

            /// <summary>
            ///     Creates a new <see cref="FileNode" /> with the specified object.
            /// </summary>
            /// <typeparam name="T">The type of the object's value.</typeparam>
            /// <param name="name">The name of the object.</param>
            /// <param name="value">The value of the object.</param>
            /// <returns></returns>
            protected static FileNode Create<T>
                (string name,
                 T value)
                where T : class, ISelfSerializable
                {
                return new FileNode(value, name);
                }
        }
    }
}