using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory
{
    /// <summary>
    /// Container with type and instance reference
    /// </summary>
    internal class ContainerValue : IDisposable
    {
        /// <summary>
        /// Factory registered class type
        /// </summary>
        internal Type ValueType = null;
        /// <summary>
        /// Reference of instance 
        /// </summary>
        internal object Instance = null;
        /// <summary>
        /// Instance recreation flag
        /// </summary>
        internal bool Rebuild = false;

        public void Dispose()
        {
            ValueType = null;
            if (Instance is IDisposable todispose)
                todispose?.Dispose();
            Instance = null;
        }
        /// <summary>
        /// Get instance from container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal T GetValue<T>()
        {
            if (Rebuild)
            {
                var instance = Activator.CreateInstance(ValueType);
                return (T)instance;
            }
            else
            {
                if (Instance == null)
                    Instance = Activator.CreateInstance(ValueType);
                return (T)Instance;
            }            
        }
    }
}
