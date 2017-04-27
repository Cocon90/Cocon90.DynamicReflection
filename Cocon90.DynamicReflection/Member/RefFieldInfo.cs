using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Cocon90.DynamicReflection.Helper;

namespace Cocon90.DynamicReflection.Member
{
    internal delegate object FieldGetDelegate(object obj);
    internal delegate void FieldSetDelegate(object obj, object value);

    public class RefFieldInfo : IDynamicFieldInfo
    {
        Type type;
        FieldInfo info;
        FieldSetDelegate setDelegate = null;
        FieldGetDelegate getDelegate = null;
        public FieldInfo FieldInfo { get { return info; } }
        public RefFieldInfo(Type type, FieldInfo info)
        {
            this.type = type;
            this.info = info;
        }
        public string Name => info.Name;

        public Type ObjectType => info.FieldType;

        public object GetValue(object obj)
        {
            if (this.getDelegate != null)
            {
                return this.getDelegate(obj);
            }
            this.getDelegate = DynamicMethodHelper.CreateGetHandler(type, info);
            return this.getDelegate(obj);
        }

        public void SetValue(object obj, object value)
        {
            if (this.setDelegate != null)
            {
                this.setDelegate(obj, value);
                return;
            }
            this.setDelegate = DynamicMethodHelper.CreateSetHandler(type, info);
            this.setDelegate(obj, value);
        }
    }
}
