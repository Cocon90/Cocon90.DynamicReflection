using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Cocon90.Test.Core
{
    public class MyClass
    {
        public MyClass() { }
        public string Name { get; set; }
        public Guid? Id { get; set; }
        public DateTime? Date { get; set; }
    }
    public class UnitTest1
    {
        [Fact]
        public void TestConstruction1()
        {
            var type = typeof(MyClass);
            DynamicReflection.DynamicType dy = new DynamicReflection.DynamicType(type);
            var constructior = dy.GetConstructor(new Type[0]);
            for (int i = 0; i < 10000000; i++)
            {
                var t = constructior.Invoke(new object[] { });
            }
        }
        [Fact]
        public void TestConstruction2()
        {
            var type = typeof(MyClass);
            //var constructior = type.GetType().GetTypeInfo().GetConstructor(new Type[0]);
            for (int i = 0; i < 10000000; i++)
            {
                var t = Activator.CreateInstance(type);
                //var t = (MyClass)constructior.Invoke(new object[] { });
            }
        }

        [Fact]
        public void TestGetProperty1()
        {
            var type = typeof(MyClass);
            DynamicReflection.DynamicType dy = new DynamicReflection.DynamicType(type);
            var pro = dy.GetProperty("Id");
            var obj = new MyClass { Id = null };
            for (int i = 0; i < 10000000; i++)
            {
                var val = pro.GetValue(obj, null);
            }
        }
        [Fact]
        public void TestGetProperty2()
        {
            var type = typeof(MyClass);
            var pro = type.GetProperty("Id");
            var obj = new MyClass { Id = null };
            for (int i = 0; i < 10000000; i++)
            {
                var val = pro.GetValue(obj, null);
            }
        }
        [Fact]
        public void TestSetProperty1()
        {
            var type = typeof(MyClass);
            DynamicReflection.DynamicType dy = new DynamicReflection.DynamicType(type);
            var pro = dy.GetProperty("Id");
            var obj = new MyClass { Id = Guid.NewGuid() };
            var newVal = Guid.NewGuid();
            for (int i = 0; i < 2300000; i++)
            {
                pro.SetValue(obj, newVal, null);
            }
        }
        [Fact]
        public void TestSetProperty2()
        {
            var type = typeof(MyClass);
            var pro = type.GetProperty("Id");
            var obj = new MyClass { Id = Guid.NewGuid() };
            var newVal = Guid.NewGuid();
            for (int i = 0; i < 2300000; i++)
            {
                pro.SetValue(obj, newVal, null);
            }
        }
    }
}
