using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TinyFactory.Exceptions;

namespace TinyFactory
{
    public static class ServiceDescriptorExt
    {
        /// <summary>
        /// Method for getting an instance of a type
        /// </summary>
        /// <param name="descriptor"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static object Resolve(this ServiceDescriptor descriptor, IFactoryProvider provider)
        {
            switch (descriptor.Lifetime)
            {
                case ServiceLifetime.Transient:
                case ServiceLifetime.TransientFirstLoader:
                    return descriptor.GetInstance(provider);
                case ServiceLifetime.Singleton:
                case ServiceLifetime.HostedService:
                case ServiceLifetime.SingletonFirstLoader:
                    return descriptor.ImplementationInstance ??
                        (descriptor.ImplementationInstance = descriptor.GetInstance(provider));
                default:
                    return null;
            }
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="descriptor"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static object GetInstance(this ServiceDescriptor descriptor, IFactoryProvider provider)
        {
            ConstructorInfo item = descriptor.ImplementationType.GetConstructors().FirstOrDefault(o => o.IsPublic && !o.IsStatic);
            if (item != null)
            {
                List<object> args = new List<object>();
                foreach (var attr in item.GetParameters())
                {
                    var attr_type = attr.ParameterType;
                    args.Add(provider.Get(attr_type) ?? attr.DefaultValue);
                }

                return args.Count > 0 ?
                    Activator.CreateInstance(descriptor.ImplementationType, args.ToArray()) :
                    Activator.CreateInstance(descriptor.ImplementationType);
            }
            else
            {
                if (provider.ThrowNotExist) 
                    throw new FactoryConfigurationException($"Implementation {descriptor.ImplementationType.Name} not have public constructors");
                else
                    return default;
            }
        }
    }
}
