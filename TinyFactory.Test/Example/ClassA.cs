using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Test.Example
{
    public class ClassA : InterfaceA
    {
        public int Value { get; set; }

        public ClassA()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            Value = rnd.Next(0, rnd.Next(1000, 9000));
        }
        private ClassA(int val)
        {
            Value = val;
        }

        public static ClassA Build(int value) => new ClassA(value);
    }
}
