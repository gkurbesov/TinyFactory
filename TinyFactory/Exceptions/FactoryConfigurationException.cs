using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Exceptions
{
    public class FactoryConfigurationException : Exception
    {
        public FactoryConfigurationException() { }
        public FactoryConfigurationException(string message)
        : base(message) { }
        public FactoryConfigurationException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}
