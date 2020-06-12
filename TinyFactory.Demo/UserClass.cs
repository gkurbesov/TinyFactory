using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Demo
{
    public class UserClass
    {
        public string Name { get; set; } = string.Empty;
        
        public UserClass()
        {
            Name = "EMPTY NAME";
        }
        public UserClass(string name)
        {
            Name = name;
        }

    }
}
