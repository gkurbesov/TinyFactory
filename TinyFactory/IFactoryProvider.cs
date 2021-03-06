﻿using System;

namespace TinyFactory
{
    /// <summary>
    /// Factory Provider
    /// </summary>
    public interface IFactoryProvider
    {
        bool ThrowNotExist { get;}
        /// <summary>
        /// Get type instance from factory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>() where T : class;
        /// <summary>
        /// Get type instance from factory
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Get(Type type);
    }
}
