using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TinyFactory.Background;
using TinyFactory.Exceptions;
using TinyFactory.FirstLoader;

namespace TinyFactory
{
    /// <summary>
    ///  TinyFactory container
    /// </summary>
    public abstract class TinyFactoryService : IFactoryProvider
    {
        private readonly IFactoryCollection collections;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public bool ThrowNotExist { get; private set; } = false;

        public TinyFactoryService(bool throwIfNotExist = false)
        {
            ThrowNotExist = throwIfNotExist;
            collections = new FactoryCollection();

            ConfigureFactory(collections);
            collections.Build();

            StartFirstLoaders();
            StartHostedServices();
        }

        /// <summary>
        /// Loads and execute sequentially the Hosted Services
        /// </summary>
        private void StartHostedServices()
        {
            var descriptors = collections.Where(o => o.Lifetime == ServiceLifetime.HostedService);
            foreach (var descriptor in descriptors)
            {
                var instance = descriptor.Resolve(this);
                if (instance is IHostedService service)
                    service.StartAsync(cancellationTokenSource.Token)
                        .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Loads and calls sequentially the First Loaders
        /// </summary>
        private void StartFirstLoaders()
        {
            var descriptors = collections.Where(o => o.Lifetime == ServiceLifetime.SingletonFirstLoader || o.Lifetime == ServiceLifetime.TransientFirstLoader);
            foreach (var descriptor in descriptors)
            {
                var instance = descriptor.Resolve(this);
                if (instance is IFirstLoader loader)
                    loader.Execute();
            }
        }

        /// <summary>
        /// Factory configuration method
        /// </summary>
        /// <param name="collection"></param>
        protected abstract void ConfigureFactory(IFactoryCollection collection);

        /// <summary>
        /// Get type instance from factory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : class =>
            (T)Get(typeof(T));

        /// <summary>
        /// Get type instance from factory
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Get(Type type)
        {
            if (!collections.IsBuild)
                throw new FactoryConfigurationException("TinyFactory is not configured");
            var descriptor = collections.FirstOrDefault(o => o.ImplementationType.Equals(type) || o.ServiceType.Equals(type));
            if (ThrowNotExist && descriptor == null)
                throw new FactoryConfigurationException($"Unknown parameter type ({type.Name})");
            return descriptor?.Resolve(this);
        }
    }
}
