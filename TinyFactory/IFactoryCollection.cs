using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory
{
    public interface IFactoryCollection : ICollection<ServiceDescriptor>
    {
        /// <summary>
        /// True - if the collection is complete to use
        /// </summary>
        bool IsBuild { get; }
        /// <summary>
        /// Fixing and building a collection of types
        /// </summary>
        /// <returns></returns>
        IFactoryCollection Build();
    }
}
