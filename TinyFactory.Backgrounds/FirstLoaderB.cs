using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Backgrounds
{
    public class FirstLoaderB
    {
        public FirstLoaderB(IDataSource data)
        {
            Print("Call constructor");
            Print($"{data.GetData()}");
            // Exaple exceptions in first loaders
            //throw new Exception("first loader text exception");
        }

        private void Print(string value)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("FirstLoaderB:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(value);
        }
    }
}
