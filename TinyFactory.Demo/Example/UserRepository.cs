using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Demo.Example
{
    public class UserRepository : IRepository
    {
        private readonly IRepositoryConfig config;
        private List<object> data = new List<object>();

        public UserRepository(IRepositoryConfig config)
        {
            this.config = config;
        }

        public string GetConnectionString() =>
            config.ConnectionString;

        public object[] GetAll() =>
            data.ToArray();

        public int Add(object value)
        {
            data.Add(value);
            return data.Count - 1;
        }

        public bool Delete(object value) =>
            data.Remove(value);

        public void Delete(int index) =>
            data.RemoveAt(index);
    }
}
