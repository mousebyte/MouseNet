using System;
using System.Collections;
using System.Collections.Generic;

namespace MouseNet.Tools
{
    /// <summary>
    ///     Wraps a list of <see cref="INamedObject" /> and provides methods for
    ///     accessing and managing them.
    /// </summary>
    /// <typeparam name="TNamedObject">The type of the named object.</typeparam>
    /// <seealso cref="IList{T}" />
    public class NamedObjectList<TNamedObject>
        : IList<TNamedObject>
        where TNamedObject : INamedObject
    {
        /// <inheritdoc />
        public NamedObjectList()
            {
            InnerList = new List<TNamedObject>();
            }

        /// <summary>
        ///     Gets the inner list.
        /// </summary>
        /// <value>
        ///     The inner list in which objects are stored.
        /// </value>
        protected List<TNamedObject> InnerList { get; }
        /// <inheritdoc />
        public int Count => InnerList.Count;
        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public void RemoveAt
            (int index)
            {
            InnerList.RemoveAt(index);
            }
        
        /// <inheritdoc />
        /// <exception cref="T:System.ArgumentException">
        ///     An object with this value already exists,
        ///     or
        ///     an object with the same name already exists at a different index.
        /// </exception>
        public TNamedObject this
            [int i] {
            get => InnerList[i];
            set {
                if (Contains(value.Value))
                    throw new ArgumentException(
                        "An object with this value already exists.");
                var existingIndex = IndexOf(value.Name);
                if (existingIndex == i || existingIndex == -1)
                    InnerList[i] = value;
                else
                    throw new ArgumentException(
                        "An object with the same name already exists "
                      + "at a different index.");
            }
        }

        /// <inheritdoc />
        public IEnumerator<TNamedObject> GetEnumerator()
            {
            return InnerList.GetEnumerator();
            }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
            {
            return GetEnumerator();
            }

        /// <summary>
        ///     Determines whether this list contains an object with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///     <c>true</c> if the list contains a matching object; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains
            (string name)
            {
            return InnerList.Exists(o => o.Name == name);
            }

        /// <summary>
        ///     Determines whether this list contains an <see cref="INamedObject" /> with a
        ///     reference to the specified object as its value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     <c>true</c> if the list contains a matching object; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains
            (object value)
            {
            return InnerList.Exists(o => o.Value.Equals(value));
            }

        /// <inheritdoc />
        public bool Contains
            (TNamedObject obj)
            {
            return InnerList.Contains(obj);
            }

        /// <inheritdoc />
        public void CopyTo
            (TNamedObject[] array,
             int arrayIndex)
            {
            InnerList.CopyTo(array, arrayIndex);
            }

        /// <inheritdoc />
        public void Add
            (TNamedObject obj)
            {
            if (Contains(obj?.Name))
                throw new ArgumentException(
                    "An object with the same name already exists.",
                    nameof(obj));
            if (Contains(obj?.Value))
                throw new ArgumentException(
                    "An object with this value already exists.");
            InnerList.Add(obj);
            }

        /// <summary>
        ///     Adds a range of objects to the end of the list.
        /// </summary>
        /// <param name="objects">The objects to add.</param>
        public void AddRange
            (IEnumerable<TNamedObject> objects)
            {
            foreach (var namedObject in objects)
                Add(namedObject);
            }

        /// <inheritdoc />
        public bool Remove
            (TNamedObject obj)
            {
            return InnerList.Remove(obj);
            }

        /// <summary>
        ///     Attempts to remove the object with the specified name
        ///     from the list.
        /// </summary>
        /// <param name="name">The name of the object to remove.</param>
        /// <returns>
        ///     <c>true</c> if the object was successfully found
        ///     and removed; otherwise, <c>false</c>.
        /// </returns>
        public bool Remove
            (string name)
            {
            return InnerList.Remove(Find(name));
            }

        /// <summary>
        ///     Attempts to remove the object with the specified value from
        ///     the list.
        /// </summary>
        /// <param name="value">The value of the object to remove.</param>
        /// <returns>
        ///     <c>true</c> if the object was successfully found
        ///     and removed; otherwise, <c>false</c>.
        /// </returns>
        public bool Remove
            (object value)
            {
            return InnerList.Remove(Find(value));
            }

        /// <inheritdoc />
        public void Clear()
            {
            InnerList.Clear();
            }

        /// <summary>
        ///     Searches for an object with the specified name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>
        ///     The <c>TNamedObject</c> with the specified name,
        ///     or the default value of <c>TNamedObject</c> if it does not.
        /// </returns>
        public TNamedObject Find
            (string name)
            {
            return InnerList.Find(o => o.Name == name);
            }

        /// <summary>
        ///     Searches for an object with the specified value.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>
        ///     The <c>TNamedObject</c> with the specified value,
        ///     or the default value of <c>TNamedObject</c> if it does not.
        /// </returns>
        public TNamedObject Find
            (object value)
            {
            return InnerList.Find(o => o.Value == value);
            }

        /*public TNamedObject Find
            (TNamedObject obj)
            {
            return InnerList.Find(o => o.Equals(obj));
            }*/

        /// <summary>
        ///     Searches for the object with the specified name and returns its index.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>
        ///     The index of the <see cref="INamedObject" /> with the specified
        ///     name, or <c>-1</c> if it does not exist.
        /// </returns>
        public int IndexOf
            (string name)
            {
            return InnerList.FindIndex(o => o.Name == name);
            }

        /// <summary>
        ///     Searches for the object with the specified value and returns its index.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>
        ///     The index of the <see cref="INamedObject" /> with the specified
        ///     value, or <c>-1</c> if it does not exist.
        /// </returns>
        public int IndexOf
            (object value)
            {
            return InnerList.FindIndex(o => o.Value.Equals(value));
            }

        /// <inheritdoc />
        public int IndexOf
            (TNamedObject obj)
            {
            return InnerList.IndexOf(obj);
            }

        /// <inheritdoc />
        public void Insert
            (int index,
             TNamedObject item)
            {
            InnerList.Insert(index, item);
            }

        /// <summary>
        ///     Returns a shallow copy of a range of objects in the list.
        /// </summary>
        /// <param name="start">The starting index of the range.</param>
        /// <param name="count">The number of objects to return.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{T}" /> containing the objects
        ///     in the specified range.
        /// </returns>
        public IEnumerable<TNamedObject> GetRange
            (int start,
             int count)
            {
            return InnerList.GetRange(start, count);
            }
    }

    /// <inheritdoc cref="NamedObjectList{TNamedObject}" />
    /// <summary>
    ///     Specializes the <see cref="NamedObjectList{TNamedObject}" /> class for the type
    ///     <see cref="INamedObject{TValue}" />.
    /// </summary>
    /// <typeparam name="TValue">The type of the contained objects' values.</typeparam>
    /// <seealso cref="NamedObjectList{TNamedObject}" />
    /// <seealso cref="IEnumerable{T}" />
    /// <remarks>
    ///     This class exposes its objects' values via an enumerator, allowing
    ///     them to be worked with directly.
    /// </remarks>
    public class TypedNamedObjectList<TValue>
        : NamedObjectList<INamedObject<TValue>>, IEnumerable<TValue>
    {
        /// <inheritdoc />
        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
            {
            foreach (var namedObject in InnerList)
                yield return namedObject.Value;
            }
    }
}