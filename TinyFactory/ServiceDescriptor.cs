using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory
{
    public class ServiceDescriptor
    {
        public Type ImplementationType { get; internal set; }
        public object ImplementationInstance { get; internal set; }
        public ServiceLifetime Lifetime { get; internal set; }
        public Type ServiceType { get; internal set; }

        public ServiceDescriptor(Type serviceType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            ImplementationType = serviceType;
            Lifetime = lifetime;
        }

        public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            Lifetime = lifetime;
        }

        public static ServiceDescriptor Singleton<TService>() where TService : class =>
            new ServiceDescriptor(typeof(TService), ServiceLifetime.Singleton);

        public static ServiceDescriptor Singleton<TService, TImpl>() where TService : class =>
            new ServiceDescriptor(typeof(TService), typeof(TImpl), ServiceLifetime.Singleton);

        public static ServiceDescriptor Singleton<TService>(object value) where TService : class =>
            new ServiceDescriptor(typeof(TService), ServiceLifetime.Singleton) { ImplementationInstance = value };

        public static ServiceDescriptor Transient<TService>() where TService : class =>
          new ServiceDescriptor(typeof(TService), ServiceLifetime.Transient);

        public static ServiceDescriptor Transient<TService, TImpl>() where TService : class =>
            new ServiceDescriptor(typeof(TService), typeof(TImpl), ServiceLifetime.Transient);
    }
}
