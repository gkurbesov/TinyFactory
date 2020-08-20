using System;
using System.Collections.Generic;
using System.Text;
using TinyFactory.Demo.Example;

namespace TinyFactory.Demo
{
    public class Factory : TinyFactory
    {
        private static Factory _instace;
        private static object locker = new object();
        private Factory() : base() { }

        protected override void ConfigureFactory(IFactoryCollection collection)
        {
            collection.AddSingleton<IRepository, UserRepository>();
            collection.AddTransient<IRepositoryConfig, Config>();
        }

        public static Factory GetFactory()
        {
            lock(locker)
            {
                if (_instace == null) _instace = new Factory();
                return _instace;
            }
        }

        public static T Resolve<T>() where T: class => GetFactory().Get<T>();

    }
}
