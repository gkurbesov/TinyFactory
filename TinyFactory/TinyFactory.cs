using System;
using System.Collections.Concurrent;

namespace TinyFactory
{
    /// <summary>
    ///  TinyFactory container
    /// </summary>
    public abstract class TinyFactory
    {
        private readonly IFactoryCollection collections;

        public TinyFactory()
        {
            collections = new FactoryCollection();
            ConfigureFactory(collections);
            collections.Build();
        }

        protected abstract void ConfigureFactory(IFactoryCollection collection);



        /// <summary>
        /// Get type instance from factory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : class
        {
            return null;
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
