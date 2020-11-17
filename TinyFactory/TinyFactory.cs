using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TinyFactory.Background;
using TinyFactory.Exceptions;

namespace TinyFactory
{
    /// <summary>
    ///  TinyFactory container
    /// </summary>
    public abstract class TinyFactory : IFactoryProvider
    {
        private readonly IFactoryCollection collections;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public bool ThrowNotExist { get; private set; } = false;

        public TinyFactory(bool throwIfNotExist = false)
        {
            ThrowNotExist = throwIfNotExist;
            collections = new FactoryCollection();

            ConfigureFactory(collections);
            collections.Build();

            StartFirstLoaders();
            StartHostedServices();
        }
        
        private void StartHostedServices()
        {
            var descriptors = collections.Where(o => o.Lifetime == ServiceLifetime.HostedService);
            foreach (var descriptor in descriptors)
            {
                var instance = descriptor.Resolve(this);
                if (instance is IHostedService service)
                {
                    service.StartAsync(cancellationTokenSource.Token).ConfigureAwait(false);
                }
            }
        }

        private void StartFirstLoaders()
        {
            var descriptors = collections.Where(o => o.Lifetime == ServiceLifetime.SingletonFirstLoader);
            foreach (var descriptor in descriptors)
                descriptor.Resolve(this);
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

        #region deprecated
        /// <summary>
        /// Puts objects in a dictionary
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="rebuild"></param>
        [Obsolete("AddToValues ​​is deprecated, please override and use FactoryConfigure", true)]
        private void AddToValues(Type type, object obj, bool rebuild) { }
        /// <summary>
        /// Registers a class type. This type of class will be recreated with every resolve.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        [Obsolete("Register<T> ​​is deprecated, please override and use FactoryConfigure", true)]
        protected void Register<T>() where T : class { }
        /// <summary>
        ///  Registers a class type as singleton
        /// </summary>
        /// <typeparam name="T"></typeparam>
        [Obsolete("Singleton<T> ​​is deprecated, please override and use FactoryConfigure", true)]
        protected void Singleton<T>() where T : class { }
        /// <summary>
        /// Registers object as singleton
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        [Obsolete("Singleton<T>(T value) ​​is deprecated, please override and use FactoryConfigure", true)]
        protected void Singleton<T>(T value) where T : class { }
        /// <summary>
        /// Removes a type or instance from the factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        [Obsolete("Remove ​​is deprecated", true)]
        protected void Remove<T>() { }
        #endregion
    }
}
