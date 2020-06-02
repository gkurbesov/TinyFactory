using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory
{
    internal class ContainerValue : IDisposable
    {
        internal Type ValueType = null;
        internal object RawValue = null;
        internal bool Rebuild = false;

        public void Dispose()
        {
            ValueType = null;
            if (RawValue is IDisposable todispose)
                todispose?.Dispose();
            RawValue = null;
        }

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
