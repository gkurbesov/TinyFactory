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
        internal object RawValue = null;
        /// <summary>
        /// Instance recreation flag
        /// </summary>
        internal bool Rebuild = false;

        public void Dispose()
        {
            ValueType = null;
            if (RawValue is IDisposable todispose)
                todispose?.Dispose();
            RawValue = null;
        }
        /// <summary>
        /// Get instance from container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal T GetValue<T>()
        {
            if (Rebuild && RawValue != null)
            {
                if (RawValue is IDisposable todispose)
                    todispose?.Dispose();
                RawValue = null;
            }
            if (RawValue == null)
                RawValue = Activator.CreateInstance(ValueType);
            return (T)RawValue;
        }
    }
}
