using System;
using System.Collections.Concurrent;

namespace TinyFactory
{
    /// <summary>
    ///  
    /// </summary>
    public class TinyFactory : IDisposable
    {
        /// <summary>
        /// Dictionary with types and instances
        /// </summary>
        private ConcurrentDictionary<Type, ContainerValue> values = new ConcurrentDictionary<Type, ContainerValue>();
        /// <summary>
        /// Puts objects in a dictionary
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="rebuild"></param>
        private void AddToValues(Type type, object obj, bool rebuild)
        {
            var value = new ContainerValue()
            {
                ValueType = type,
                Instance = obj,
                Rebuild = rebuild
            };
            if (!values.TryAdd(type, value))
            {
                value.Dispose();
                throw new Exception("Failed to register type");
            }
        }
        /// <summary>
        /// Checks for type presence in a dictionary
        /// </summary>
        /// <param name="type"></param>
        private void ContainsType(Type type)
        {
            if (values.ContainsKey(type))
                throw new Exception("This class type has been registered to the factory before");
        }
        /// <summary>
        /// Checks for a public constructor
        /// </summary>
        /// <param name="type"></param>
        private void ConstructorExist(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) == null)
                throw new Exception("The class type must contain an empty public constructor or using Singlton<T>(T value)");
        }
        /// <summary>
        /// Registers a class type. This type of class will be recreated with every resolve.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void Register<T>() where T : class
        {
            var insert_type = typeof(T);
            ContainsType(insert_type);
            ConstructorExist(insert_type);
            AddToValues(insert_type, null, true);
        }
        /// <summary>
        ///  Registers a class type as singlton
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void Singlton<T>() where T : class
        {
            var insert_type = typeof(T);
            ContainsType(insert_type);
            ConstructorExist(insert_type);
            AddToValues(insert_type, null, false);
        }
        /// <summary>
        /// Registers object as singleton
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        protected void Singlton<T>(T value) where T : class
        {
            var insert_type = typeof(T);
            ContainsType(insert_type);
            if (value == null)
                throw new ArgumentNullException("Singlton value cannot contain null");
            AddToValues(insert_type, value, false);
        }
        /// <summary>
        /// Removes a type or instance from the factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
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
        /// <summary>
        /// Get type instance from factory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
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
