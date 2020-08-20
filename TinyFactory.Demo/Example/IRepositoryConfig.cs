using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Demo.Example
{
    public interface IRepositoryConfig
    {
        string ConnectionString { get; set; }
    }
}
