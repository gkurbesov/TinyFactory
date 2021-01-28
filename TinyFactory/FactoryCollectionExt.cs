using System;
using System.Linq;
using TinyFactory.Background;
using TinyFactory.FirstLoader;

namespace TinyFactory
{
    public static class FactoryCollectionExt
    {
        public static IFactoryCollection AddFirstLoader<TService>(this IFactoryCollection collection, bool singleton = true) where TService : class, IFirstLoader
        {
            if (collection.AllowAddServiceToCollection<TService>())
                collection.Add(ServiceDescriptor.FirstLoader<TService>(singleton));
            return collection;
        }

        public static IFactoryCollection AddSingleton<TService>(this IFactoryCollection collection) where TService : class
        {
            if (collection.AllowAddServiceToCollection<TService>())
                collection.Add(ServiceDescriptor.Singleton<TService>());
            return collection;
        }

        public static IFactoryCollection AddSingleton<TService>(this IFactoryCollection collection, object instance) where TService : class
        {
            if (instance == null)
                throw new ArgumentNullException("Singleton instance cannot be null");

            if (collection.AllowAddServiceToCollection<TService>(instance.GetType(), true))
                collection.Add(ServiceDescriptor.Singleton<TService>(instance));

            return collection;
        }

        public static IFactoryCollection AddSingleton<TService, TImpl>(this IFactoryCollection collection) where TService : class where TImpl : class, TService
        {
            if (collection.AllowAddServiceToCollection<TService, TImpl>())
                collection.Add(ServiceDescriptor.Singleton<TService, TImpl>());
            return collection;
        }

        public static IFactoryCollection AddTransient<TService>(this IFactoryCollection collection) where TService : class
        {
            if (collection.AllowAddServiceToCollection<TService>())
                collection.Add(ServiceDescriptor.Transient<TService>());
            return collection;
        }

        public static IFactoryCollection AddTransient<TService, TImpl>(this IFactoryCollection collection) where TService : class where TImpl : class, TService
        {
            if (collection.AllowAddServiceToCollection<TService, TImpl>())
                collection.Add(ServiceDescriptor.Transient<TService, TImpl>());
            return collection;
        }


        public static IFactoryCollection AddHostedService<TService>(this IFactoryCollection collection) where TService : class, IHostedService
        {
            if (collection.AllowAddServiceToCollection<TService>())
                collection.Add(ServiceDescriptor.HostedService<TService>());
            return collection;
        }

        public static IFactoryCollection AddHostedService<TService>(this IFactoryCollection collection, TService instance) where TService : class, IHostedService
        {
            if (instance == null)
                throw new ArgumentNullException("HostedService instance cannot be null");

            if (collection.AllowAddServiceToCollection<TService>(instance.GetType()))
                collection.Add(ServiceDescriptor.HostedService<TService>());

            return collection;
        }


        internal static bool AllowAddServiceToCollection<TService>(this IFactoryCollection collection)
        {
            if (collection.IsReadOnly)
                throw new InvalidOperationException("You cannot add to collection. Factory Collection is read-only");

            if (collection.FirstOrDefault(o => o.ServiceType.Equals(typeof(TService))) != null)
                throw new Exception("This type of service has been registered to the factory before");

            var constructors = typeof(TService).GetConstructors();
            if (constructors == null || constructors.Length == 0)
                throw new Exception("This type of service has no public constructors");

            return true;
        }

        internal static bool AllowAddServiceToCollection<TService>(this IFactoryCollection collection, Type imptType, bool isInstance = false)
        {
            if (collection.IsReadOnly)
                throw new InvalidOperationException("You cannot add to collection. Factory Collection is read-only");

            if (collection.FirstOrDefault(o => o.ServiceType.Equals(typeof(TService))) != null)
                throw new Exception("This type of service has been registered to the factory before");

            if (!isInstance)
            {
                var constructors = imptType.GetConstructors();
                if (constructors == null || constructors.Length == 0)
                    throw new Exception("This type of service has no public constructors");
            }

            return true;
        }

        internal static bool AllowAddServiceToCollection<TService, TImpl>(this IFactoryCollection collection)
        {
            if (collection.IsReadOnly)
                throw new InvalidOperationException("You cannot add to collection. Factory Collection is read-only");

            if (collection.FirstOrDefault(o => o.ServiceType.Equals(typeof(TService)) || o.ImplementationType.Equals(typeof(TService)) ||
            o.ServiceType.Equals(typeof(TImpl)) || o.ImplementationType.Equals(typeof(TImpl))) != null)
                throw new Exception("This type of service has been registered to the factory before");

            var constructors = typeof(TImpl).GetConstructors();
            if (constructors == null || constructors.Length == 0)
                throw new Exception("This type of service has no public constructors");

            return true;
        }
    }
}
