using System;
using System.Collections.Generic;
using System.Text;

namespace Cocon90.DynamicReflection
{
    public interface IDynamicFieldInfo
    {
        string Name { get; }
        Type ObjectType { get; }
        object GetValue(object obj);
        void SetValue(object obj, object value);
    }
}
