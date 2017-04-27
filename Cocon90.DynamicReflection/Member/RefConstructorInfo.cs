using System;
using System.Collections.Generic;
using System.Text;
using Cocon90.DynamicReflection.Helper;
using System.Reflection;

namespace Cocon90.DynamicReflection.Member
{
    internal delegate object ConstructorInfoDelegate(object[] parameters);

    public class RefConstructorInfo : IDynamicConstructorInfo
    {
        Type type = null;
        ConstructorInfo info;
        ConstructorInfoDelegate delgate = null;
        public ConstructorInfo ConstructorInfo { get { return info; } }
        public RefConstructorInfo(Type type, ConstructorInfo info)
        {
            this.type = type;
            this.info = info;
        }

        public object Invoke(object[] parameters)
        {
            if (delgate != null)
            {
                return delgate(parameters);
            }
            this.delgate = DynamicMethodHelper.CreateDynamicConstructorInfoHandler(type, info);
            return this.delgate(parameters);
        }
    }
}
