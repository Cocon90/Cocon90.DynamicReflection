using System;
using System.Collections.Generic;
using System.Text;

namespace Cocon90.DynamicReflection
{
    public interface IDynamicPropertyInfo
    {
        string Name { get; }
        Type ObjectType { get; }
        object GetValue(object obj, object[] index);
        void SetValue(object obj, object value, object[] index);
    }
}
