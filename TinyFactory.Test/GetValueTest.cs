using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace TinyFactory.Test
{
    public class GetValueTest : TinyFactory
    {
        public GetValueTest()
        {
            Register<ExampleClass>();
        }

        [Fact]
        public void GetNewInstanceTest()
        {
            var a = Get<ExampleClass>();
            var b = Get<ExampleClass>();

            Assert.NotEqual(a, b);
        }

        [Fact]
        public void GetNewInstanceTest2()
        {
            var a = Get<ExampleClass>();
            var b = Get<ExampleClass>();

            Assert.NotEqual(a.Count, b.Count);
        }

        [Fact]
        public void GetSingltonTest()
        {
            Singleton<TimeStringClass>();

            var a = Get<TimeStringClass>();
            Thread.Sleep(1000);
            var b = Get<TimeStringClass>();

            Assert.Equal(a.Value, b.Value);
        }

        [Fact]
        public void GetSingltonTest2()
        {
            var num = new Random().Next(0, 1000);
            Singleton(ExampleNoConstructor.Build(num));

            var a = Get<ExampleNoConstructor>();

            Assert.Equal(num, a.Count);
        }
    }
}
