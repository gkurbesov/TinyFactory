using System;
using System.Collections.Concurrent;

namespace TinyFactory
{
    public class TinyFactory : IDisposable
    {
        private ConcurrentDictionary<Type, ContainerValue> values = new ConcurrentDictionary<Type, ContainerValue>();

        private void AddToValues(Type type, object obj, bool rebuild)
        {
            var value = new ContainerValue()
            {
                ValueType = type,
                RawValue = obj,
                Rebuild = rebuild
            };
            if (!values.TryAdd(type, value))
            {
                value.Dispose();
                throw new Exception("Failed to register type");
            }
        }

        private void ContainsType(Type type)
        {
            if (values.ContainsKey(type))
                throw new Exception("This class type has been registered to the factory before");
        }

        private void ConstructorExist(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) == null)
                throw new Exception("The class type must contain an empty public constructor or using Singlton<T>(T value)");
        }

        protected void Register<T>() where T : class
        {
            var insert_type = typeof(T);
            ContainsType(insert_type);
            ConstructorExist(insert_type);
            AddToValues(insert_type, null, true);
        }

        protected void Singlton<T>() where T : class
        {
            var insert_type = typeof(T);
            ContainsType(insert_type);
            ConstructorExist(insert_type);
            AddToValues(insert_type, null, false);
        }

        protected void Singlton<T>(T value) where T : class
        {
            var insert_type = typeof(T);
            ContainsType(insert_type);
            if (value == null)
                throw new ArgumentNullException("Singlton value cannot contain null");
            AddToValues(insert_type, value, false);
        }

        protected void Remove<T>()
        {
            var remove_type = typeof(T);
            ContainsType(remove_type);
            if(values.TryRemove(remove_type, out var container))
            {
                container.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public T Get<T>() where T : class
        {
            if (values.TryGetValue(typeof(T), out var container))
                return container.GetValue<T>();
            else
                throw new Exception("This class type has not registered");
        }

        public void Dispose()
        {
            foreach(var item in values.ToArray())
                item.Value.Dispose();
            values.Clear();
            values = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
