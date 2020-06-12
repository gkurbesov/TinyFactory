using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Test
{
    public class ExampleNoConstructor
    {
        public int Count { get; set; } = 0;
        private ExampleNoConstructor(int value)
        { 
            Count = value; 
        }
        public static ExampleNoConstructor Build(int value) => new ExampleNoConstructor(value);
    }
}
