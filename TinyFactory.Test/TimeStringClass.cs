using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Test
{
    public class TimeStringClass
    {
        public string Value { get; set; }
        public TimeStringClass()
        {
            Value = DateTime.Now.ToString();
        }
    }
}
