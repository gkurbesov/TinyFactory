using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Demo
{
    public class RandomNumber
    {
        public int Number { get; set; } = 0;
        public  RandomNumber()
        {
            Number = new Random().Next(0, 10000);
        }
    }
}
