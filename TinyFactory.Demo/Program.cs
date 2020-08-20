using System;
using System.Reflection;
using TinyFactory.Demo.Example;

namespace TinyFactory.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = Factory.Resolve<UserRepository>();

            Console.WriteLine($"Config of repo = {repo.GetConnectionString()}\r\n");
            Console.WriteLine("Add John and Bob in repo");
            repo.Add("John");
            repo.Add("Bob");
            repo = null;

            GC.Collect();

            Console.WriteLine("five transient queries:");
            for (int i = 0; i < 5; i++)
                Console.WriteLine($"get {i} = {Factory.Resolve<Config>().ConnectionString}");

            Console.WriteLine("\r\n");


            Console.WriteLine("Get repo as interface and print list");
            var repo2 = Factory.Resolve<IRepository>();
            Console.WriteLine($"Config of repo = {repo2.GetConnectionString()}\r\n");

            foreach (var user in repo2.GetAll())
                Console.WriteLine(user.ToString());

            Console.WriteLine("\r\n");

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
