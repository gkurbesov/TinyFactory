using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyFactory.Test.Example;
using Xunit;

namespace TinyFactory.Test
{
    public class FactoryCollectionTest
    {
        [Fact]
        public void AddTest()
        {
            IFactoryCollection collection = new FactoryCollection();
            collection.Add(new ServiceDescriptor(typeof(ClassA), ServiceLifetime.Transient));
            collection.Add(new ServiceDescriptor(typeof(ClassB), ServiceLifetime.Singleton));

            var a = collection.FirstOrDefault(o => o.ImplementationType.Equals(typeof(ClassA)));
            var b = collection.FirstOrDefault(o => o.ImplementationType.Equals(typeof(ClassB)));

            Assert.NotNull(a);
            Assert.NotNull(b);
        }

        [Fact]
        public void CountTest()
        {
            IFactoryCollection collection = new FactoryCollection();
            collection.Add(new ServiceDescriptor(typeof(ClassA), ServiceLifetime.Transient));
            collection.Add(new ServiceDescriptor(typeof(ClassB), ServiceLifetime.Singleton));

            Assert.Equal(2, collection.Count);
            Assert.Equal(2, collection.Count());
            Assert.Equal(1, collection.Count(o=>o.Lifetime == ServiceLifetime.Singleton));
        }

        [Fact]
        public void BuildTest()
        {
            IFactoryCollection collection = new FactoryCollection();
            collection.Add(new ServiceDescriptor(typeof(ClassA), ServiceLifetime.Transient));
            collection.Build();
            collection.Add(new ServiceDescriptor(typeof(ClassB), ServiceLifetime.Singleton));

            Assert.True(collection.IsBuild);
            Assert.Equal(1, collection.Count);
        }

        [Fact]
        public void AddSingletonTest1()
        {
            IFactoryCollection collection = new FactoryCollection();
            collection.AddSingleton<ClassA>();

            var a = collection.FirstOrDefault(o => o.ImplementationType.Equals(typeof(ClassA)));

            Assert.NotNull(a);
            Assert.Equal(ServiceLifetime.Singleton, a.Lifetime);
            Assert.Equal(typeof(ClassA), a.ServiceType);
            Assert.Equal(typeof(ClassA), a.ImplementationType);
        }

        [Fact]
        public void AddSingletonTest2()
        {
            IFactoryCollection collection = new FactoryCollection();
            collection.AddSingleton<InterfaceA>(new ClassA());

            var a = collection.FirstOrDefault(o => o.ServiceType.Equals(typeof(InterfaceA)));

            Assert.NotNull(a);
            Assert.Equal(ServiceLifetime.Singleton, a.Lifetime);
            Assert.Equal(typeof(InterfaceA), a.ServiceType);
            Assert.Equal(typeof(InterfaceA), a.ImplementationType);
        }

        [Fact]
        public void AddSingletonTest3()
        {
            IFactoryCollection collection = new FactoryCollection();
            collection.AddSingleton<InterfaceA, ClassA>();

            var a = collection.FirstOrDefault(o => o.ImplementationType.Equals(typeof(ClassA)));

            Assert.NotNull(a);
            Assert.Equal(ServiceLifetime.Singleton, a.Lifetime);
            Assert.Equal(typeof(InterfaceA), a.ServiceType);
            Assert.Equal(typeof(ClassA), a.ImplementationType);
        }

        [Fact]
        public void AddTransientTest1()
        {
            IFactoryCollection collection = new FactoryCollection();
            collection.AddTransient<ClassA>();

            var a = collection.FirstOrDefault(o => o.ImplementationType.Equals(typeof(ClassA)));

            Assert.NotNull(a);
            Assert.Equal(ServiceLifetime.Transient, a.Lifetime);
            Assert.Equal(typeof(ClassA), a.ServiceType);
            Assert.Equal(typeof(ClassA), a.ImplementationType);
        }

        [Fact]
        public void AddTransientTest2()
        {
            IFactoryCollection collection = new FactoryCollection();
            collection.AddTransient<InterfaceA, ClassA>();

            var a = collection.FirstOrDefault(o => o.ImplementationType.Equals(typeof(ClassA)));

            Assert.NotNull(a);
            Assert.Equal(ServiceLifetime.Transient, a.Lifetime);
            Assert.Equal(typeof(InterfaceA), a.ServiceType);
            Assert.Equal(typeof(ClassA), a.ImplementationType);
        }
    }
}
