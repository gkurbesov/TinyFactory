using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Exceptions
{
    public class ExecutingBackgroundException : Exception
    {
        public ExecutingBackgroundException() { }
        public ExecutingBackgroundException(string message)
        : base(message) { }
        public ExecutingBackgroundException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}
