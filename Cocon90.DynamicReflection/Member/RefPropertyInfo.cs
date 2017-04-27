using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Cocon90.DynamicReflection.Helper;

namespace Cocon90.DynamicReflection.Member
{
    internal delegate void PropertySetDelegate(object obj, object value, object[] index);

    internal delegate object PropertyGetDelegate(object obj, object[] index);

    public class RefPropertyInfo : IDynamicPropertyInfo
    {
        Type type;
        PropertyInfo info;
        PropertySetDelegate setDelegate = null;
        PropertyGetDelegate getDelegate = null;
        public PropertyInfo PropertyInfo { get { return info; } }
        public RefPropertyInfo(Type type, PropertyInfo info)
        {
            this.type = type;
            this.info = info;
        }

        public string Name => info.Name;
        public Type ObjectType => info.PropertyType;
        public object GetValue(object obj, object[] index)
        {
            if (this.getDelegate != null)
            {
                return this.getDelegate(obj, index);
            }
            this.getDelegate = DynamicMethodHelper.CreateGetHandler(type, info);
            return this.getDelegate(obj, index);
        }

        public void SetValue(object obj, object value, object[] index)
        { 
            if (this.setDelegate != null)
            {
                this.setDelegate(obj, value, index);

                return;
            }
            this.setDelegate = DynamicMethodHelper.CreateSetHandler(type, info);
            this.setDelegate(obj, value, index);
        }
    }
}
