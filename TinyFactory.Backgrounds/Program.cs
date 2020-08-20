using System;

namespace TinyFactory.Backgrounds
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start factory. Pres Any key to exit...");

            var factory = new Factory();
            Console.ReadLine();
        }
    }
}
