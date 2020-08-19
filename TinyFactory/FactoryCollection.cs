using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory
{
    class FactoryCollection : IFactoryCollection
    {
        private List<ServiceDescriptor> collection = new List<ServiceDescriptor>();
        private bool IsBuild = false;

        public int Count => collection.Count;
        public bool IsReadOnly => IsBuild;


        public IFactoryCollection Build()
        {
            IsBuild = true;
            return this;
        }

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
    }
}
