# TinyFactory
Small and simple factory. TinyFactory is ready to store and create instances of simple classes that have no dependencies in the constructor.
This factory can create new instances of classes or store their references to them.

## Quick start
Create your a class factory and extend from `TinyFactory`:
```C#
public class Factory : TinyFactory
{
    private static Factory _instace;
    private static object locker = new object();
    private  Factory()
    {
        Singlton(new UserClass("John"));
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
```

And using this class in your code:
```C#
var user = Factory.Resolve<UserClass>();
Console.WriteLine($"User name: {user.Name}");
```

See more in demo project
