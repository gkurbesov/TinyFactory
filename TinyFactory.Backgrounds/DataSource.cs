using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Backgrounds
{
    public class DataSource : IDataSource
    {
        private readonly Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        public string GetData() => $"Random data from source = {rnd.Next()}";
    }
}
