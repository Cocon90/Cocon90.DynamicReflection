using System;
using System.Collections.Generic;
using System.Text;

namespace Cocon90.DynamicReflection
{
    public interface IDynamicConstructorInfo
    {
        object Invoke(object[] parameters);
    }
}
