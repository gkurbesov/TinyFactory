﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyFactory
{
    public static class FactoryCollectionExt
    {

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

            if (collection.AllowAddServiceToCollection<TService>())
                collection.Add(ServiceDescriptor.Singleton<TService>());

            return collection;
        }

        public static IFactoryCollection AddSingleton<TService, TImpl>(this IFactoryCollection collection) where TService : class
        {
            if (collection.AllowAddServiceToCollection<TService, TImpl>())
                collection.Add(ServiceDescriptor.Singleton<TService, TImpl>());
            return collection;
        }

        public static IFactoryCollection RegisterType<TService>(this IFactoryCollection collection) where TService : class
        {
            if (collection.AllowAddServiceToCollection<TService>())
                collection.Add(ServiceDescriptor.Transient<TService>());
            return collection;
        }

        public static IFactoryCollection RegisterType<TService, TImpl>(this IFactoryCollection collection) where TService : class
        {
            if (collection.AllowAddServiceToCollection<TService, TImpl>())
                collection.Add(ServiceDescriptor.Transient<TService, TImpl>());
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

        internal static bool AllowAddServiceToCollection<TService, TImpl>(this IFactoryCollection collection)
        {
            if (collection.IsReadOnly)
                throw new InvalidOperationException("You cannot add to collection. Factory Collection is read-only");

            if (collection.FirstOrDefault(o => o.ServiceType.Equals(typeof(TService)) || o.ImplementationType.Equals(typeof(TService)) || 
            o.ServiceType.Equals(typeof(TImpl)) || o.ImplementationType.Equals(typeof(TImpl))) != null)
                throw new Exception("This type of service has been registered to the factory before");

            var constructors = typeof(TService).GetConstructors();
            if (constructors == null || constructors.Length == 0)
                throw new Exception("This type of service has no public constructors");

            return true;
        }
    }
}