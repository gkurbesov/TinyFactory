using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Demo.Example
{
    public class Config : IRepositoryConfig
    {
        public string ConnectionString { get; set; }

        public Config()
        {
            var rnd = new Random();
            ConnectionString = rnd.Next(1, 99).ToString();
        }
    }
}
