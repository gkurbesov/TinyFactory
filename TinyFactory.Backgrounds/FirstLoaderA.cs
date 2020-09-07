using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Backgrounds
{
    public class FirstLoaderA
    {
        public FirstLoaderA()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("FirstLoaderA:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Call construct ");
        }
    }
}
