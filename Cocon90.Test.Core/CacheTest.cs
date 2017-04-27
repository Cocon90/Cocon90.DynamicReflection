using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Cocon90.Test.Core
{

    public class CacheTest
    {
        [Fact]
        public void TestGet()
        {
            var type = typeof(MyClass);
            DynamicReflection.ReflectionCacheManager.Instance.GetAndSet(type);
            for (int i = 0; i < 2300000; i++)
            {
                var dy = DynamicReflection.ReflectionCacheManager.Instance.GetAndSet(type);
            }
        }

    }
}
