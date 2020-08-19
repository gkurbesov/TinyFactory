﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory
{
    public interface IFactoryCollection : ICollection<ServiceDescriptor>, IEnumerable<ServiceDescriptor>, IEnumerable, IList<ServiceDescriptor>
    {
    }
}
