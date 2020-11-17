using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinyFactory.Exceptions;
using TinyFactory.Test.Example;
using Xunit;

namespace TinyFactory.Test
{
    public class TinyFactoryTest : TinyFactory
    {

        public TinyFactoryTest() : base(true) { }

        protected override void ConfigureFactory(IFactoryCollection collection)
        {
            collection.AddSingleton<InterfaceA, ClassA>();
            collection.AddTransient<InterfaceB, ClassB>();
        }

        [Fact]
        public void Test()
        {
            var a = Get<InterfaceA>();
            var b1 = Get<ClassB>();
            Task.Delay(1000).Wait();
            var b2 = Get<InterfaceB>();

            Assert.NotNull(a);
            Assert.NotNull(b1);
            Assert.NotNull(b2);
            Assert.NotNull(b1.class_a);
            Assert.Equal(a.Value, b1.class_a.Value);
            Assert.NotEqual(b1, b2);
            Assert.NotEqual(b1.Time, b2.Time);
        }

        [Fact]
        public void Test2()
        {
            Assert.Throws<FactoryConfigurationException>(() =>
            {
                Get<InterfaceC>();
            });
        }
    }
}
