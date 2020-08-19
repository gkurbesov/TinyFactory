using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Demo.Example
{
    interface IRepository
    {
        string GetConnectionString();
        object[] GetAll();
        int Add(object value);
        bool Delete(object value);
        void Delete(int index);
    }
}
