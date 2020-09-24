# TinyFactory ![](./Assets/icon.png)
<p align="center">
    <img src="./Assets/logo.png">
</p>
Small and simple factory. TinyFactory is ready to store and instantiate simple classes.

This factory can create new instances of classes or store their references to them.

TinyFactory knows how to inject dependencies through the constructor


[![Build status](https://ci.appveyor.com/api/projects/status/nvc5fkh3ua9j0mve?svg=true)](https://ci.appveyor.com/project/gkurbesov/tinyfactory)
[![Nuget](https://img.shields.io/nuget/v/TinyFactory)](https://www.nuget.org/packages/TinyFactory)
## Quick start
Create your a class factory and extend from `TinyFactory`:
```C#
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
```

And using this class in your code:
```C#
var repo = Factory.Resolve<IRepository>();
```

See more in demo project

