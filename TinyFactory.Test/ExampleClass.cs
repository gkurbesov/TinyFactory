using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Test
{
    public class ExampleClass
    {
        public int Count { get; set; } = 0;
        public ExampleClass() {
            Count = new Random().Next(0, 10000);
        }
    }
}
