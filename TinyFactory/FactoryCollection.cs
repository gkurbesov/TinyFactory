using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory
{
    class FactoryCollection : IFactoryCollection
    {
        /// <summary>
        /// list of all types
        /// </summary>
        private List<ServiceDescriptor> collection = new List<ServiceDescriptor>();
        /// <summary>
        /// True - if the collection is complete to use
        /// </summary>
        public bool IsBuild { get; private set; } = false;

        public int Count => collection.Count;
        public bool IsReadOnly => IsBuild;

        /// <summary>
        /// Fixing and building a collection of types
        /// </summary>
        /// <returns></returns>
        public IFactoryCollection Build()
        {
            IsBuild = true;
            return this;
        }

        #region Collection impl

        public void Add(ServiceDescriptor item)
        {
            if (!IsReadOnly)
                collection.Add(item);
        }

        public void Clear()
        {
            if (!IsReadOnly)
                collection.Clear();
        }

        public bool Contains(ServiceDescriptor item) =>
            collection.Contains(item);

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex) =>
            collection.CopyTo(array, arrayIndex);

        public IEnumerator<ServiceDescriptor> GetEnumerator() =>
            collection.GetEnumerator();

        public bool Remove(ServiceDescriptor item) =>
            !IsReadOnly? collection.Remove(item) : false;

        IEnumerator IEnumerable.GetEnumerator() => 
            collection.GetEnumerator();
        #endregion
    }
}
