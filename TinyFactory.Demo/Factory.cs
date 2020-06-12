using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory.Demo
{
    public class Factory : TinyFactory
    {
        private static Factory _instace;
        private static object locker = new object();
        private  Factory()
        {
            Register<RandomNumber>();
            Singleton(new UserClass("John"));
        }

        private static Factory GetFactory()
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
