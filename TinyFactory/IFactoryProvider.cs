using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory
{
    public interface IFactoryProvider
    {
        T Get<T>() where T: class;
        object Get(Type type);
    }
}
