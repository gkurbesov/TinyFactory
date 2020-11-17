using System;
using System.Collections.Generic;
using System.Text;
using TinyFactory.Test.Example;
using Xunit;

namespace TinyFactory.Test
{
    public class ServiceDescriptorTest
    {
        class MockProvider : IFactoryProvider
        {
            public bool ThrowNotExist => false;

            public T Get<T>() where T : class
            {
                throw new NotImplementedException();
            }

            public object Get(Type type)
            {
                throw new NotImplementedException();
            }
        }



        [Fact]
        public void GetInstanceTest()
        {
            var descriptor = ServiceDescriptor.Singleton<ClassA>();

            var a = descriptor.GetInstance(new MockProvider());

            Assert.NotNull(a);
            Assert.Equal(typeof(ClassA), a.GetType());
        }

        [Fact]
        public void ResolveTest1()
        {
            var descriptor = ServiceDescriptor.Singleton<ClassA>();

            ClassA a1 = (ClassA)descriptor.Resolve(new MockProvider());
            ClassA a2 = (ClassA)descriptor.Resolve(new MockProvider());
            Assert.NotNull(a1);
            Assert.NotNull(a2);
            Assert.Equal(a1, a2);
            Assert.Equal(a1.Value, a2.Value);
        }

        [Fact]
        public void ResolveTest2()
        {
            var descriptor = ServiceDescriptor.Transient<ClassA>();

            ClassA a1 = (ClassA)descriptor.Resolve(new MockProvider());
            ClassA a2 = (ClassA)descriptor.Resolve(new MockProvider());
            Assert.NotNull(a1);
            Assert.NotNull(a2);
            Assert.NotEqual(a1, a2);
            Assert.NotEqual(a1.Value, a2.Value);
        }
    }
}
