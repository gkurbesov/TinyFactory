using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Test.Example
{
    public class ClassB : InterfaceB
    {
        public string Time { get; set; }
        public InterfaceA class_a;
        public ClassB(InterfaceA instance_a)
        {
            class_a = instance_a;
            Time = DateTime.Now.ToString();
        }
    }
}
