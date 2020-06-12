using System;

namespace TinyFactory.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = Factory.Resolve<UserClass>();
            Console.WriteLine($"User name: {user.Name}");

            for(int i=0; i< 5; i++)
            {
                var number = Factory.Resolve<RandomNumber>();
                Console.WriteLine(number.Number);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
