using System;
using Xunit;

namespace TinyFactory.Test
{
    public class RegisterTest : TinyFactory
    {
        public RegisterTest ()
        {
            Register<ExampleClass>();
        }

        [Fact]
        public void DuplicateTypeTest()
        {
            var ex = Assert.Throws<Exception>(() => Register<ExampleClass>());
            Assert.Equal("This class type has been registered to the factory before", ex.Message);
        }

        [Fact]
        public void DuplicateTypeTest2()
        {
            var ex = Assert.Throws<Exception>(() => Singleton<ExampleClass>());
            Assert.Equal("This class type has been registered to the factory before", ex.Message);
        }

        [Fact]
        public void DuplicateTypeTest3()
        {
            var ex = Assert.Throws<Exception>(() => Singleton(new ExampleClass()));
            Assert.Equal("This class type has been registered to the factory before", ex.Message);
        }

        [Fact]
        public void RegistrNoConstructorTest()
        {
            var ex = Assert.Throws<Exception>(() => Register<ExampleNoConstructor>());
            Assert.Equal("The class type must contain an empty public constructor or using Singleton<T>(T value)", ex.Message);
        }

        [Fact]
        public void RegistrNoConstructorTest2()
        {
            var ex = Assert.Throws<Exception>(() => Singleton<ExampleNoConstructor>());
            Assert.Equal("The class type must contain an empty public constructor or using Singleton<T>(T value)", ex.Message);
        }

        [Fact]
        public void GetNotRegisteredTypeTest()
        {
            var ex = Assert.Throws<Exception>(() => Get<ExampleNoConstructor>());
            Assert.Equal("This class type has not registered", ex.Message);
        }
    }
}
