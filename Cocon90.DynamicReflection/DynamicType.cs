using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Cocon90.DynamicReflection.Member;

namespace Cocon90.DynamicReflection
{
    public class DynamicType : IDynamicType
    {
        Type type = null;
        public Type Type { get { return type; } }
        public DynamicType(Type type)
        {
            this.type = type;
        }


        public IDynamicConstructorInfo GetConstructor(Type[] types)
        {
            ConstructorInfo info = type.GetConstructor(types);

            if (info == null)
                return null;

            return new RefConstructorInfo(type, info);
        }

        public IDynamicConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
        {
            ConstructorInfo info = type.GetConstructor(bindingAttr, binder, types, modifiers);

            if (info == null)
                return null;

            return new RefConstructorInfo(type, info);
        }

        public IDynamicConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
        {
            ConstructorInfo info = type.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);

            if (info == null)
                return null;

            return new RefConstructorInfo(type, info);
        }

        public IDynamicConstructorInfo[] GetConstructors()
        {
            List<IDynamicConstructorInfo> list = new List<IDynamicConstructorInfo>();

            foreach (ConstructorInfo info in type.GetConstructors())
            {
                list.Add(new RefConstructorInfo(type, info));
            }

            return list.ToArray();
        }

        public IDynamicConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
        {
            List<IDynamicConstructorInfo> list = new List<IDynamicConstructorInfo>();

            foreach (ConstructorInfo info in type.GetConstructors(bindingAttr))
            {
                list.Add(new RefConstructorInfo(type, info));
            }

            return list.ToArray();
        }



        public IDynamicFieldInfo GetField(string name)
        {
            FieldInfo info = type.GetField(name);

            if (info == null)
                return null;

            return new RefFieldInfo(type, info);
        }

        public IDynamicFieldInfo GetField(string name, BindingFlags bindingAttr)
        {
            FieldInfo info = type.GetField(name, bindingAttr);

            if (info == null)
                return null;

            return new RefFieldInfo(type, info);
        }

        public IDynamicFieldInfo[] GetFields()
        {
            List<IDynamicFieldInfo> list = new List<IDynamicFieldInfo>();

            foreach (FieldInfo info in type.GetFields())
            {
                list.Add(new RefFieldInfo(type, info));
            }

            return list.ToArray();
        }

        public IDynamicFieldInfo[] GetFields(BindingFlags bindingAttr)
        {
            List<IDynamicFieldInfo> list = new List<IDynamicFieldInfo>();

            foreach (FieldInfo info in type.GetFields(bindingAttr))
            {
                list.Add(new RefFieldInfo(type, info));
            }

            return list.ToArray();
        }



        public IDynamicPropertyInfo GetProperty(string name)
        {
            PropertyInfo info = type.GetProperty(name);

            if (info == null)
                return null;

            return new RefPropertyInfo(type, info);
        }

        public IDynamicPropertyInfo GetProperty(string name, BindingFlags bindingAttr)
        {
            PropertyInfo info = type.GetProperty(name, bindingAttr);

            if (info == null)
                return null;

            return new RefPropertyInfo(type, info);
        }

        public IDynamicPropertyInfo GetProperty(string name, Type returnType)
        {
            PropertyInfo info = type.GetProperty(name, returnType);

            if (info == null)
                return null;

            return new RefPropertyInfo(type, info);
        }

 
        public IDynamicPropertyInfo GetProperty(string name, Type[] types)
        {
            PropertyInfo info = type.GetProperty(name, types);

            if (info == null)
                return null;

            return new RefPropertyInfo(type, info);
        }
 

        public IDynamicPropertyInfo GetProperty(string name, Type returnType, Type[] types)
        {
            PropertyInfo info = type.GetProperty(name, returnType, types);

            if (info == null)
                return null;

            return new RefPropertyInfo(type, info);
        }
 
        public IDynamicPropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers)
        {
            PropertyInfo info = type.GetProperty(name, returnType, types, modifiers);

            if (info == null)
                return null;

            return new RefPropertyInfo(type, info);
        }

        public IDynamicPropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
        {
            PropertyInfo info = type.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);

            if (info == null)
                return null;

            return new RefPropertyInfo(type, info);
        }
 

        public IDynamicPropertyInfo[] GetProperties()
        {
            List<IDynamicPropertyInfo> list = new List<IDynamicPropertyInfo>();

            foreach (PropertyInfo info in type.GetProperties())
            {
                list.Add(new RefPropertyInfo(type, info));
            }

            return list.ToArray();
        }

        public IDynamicPropertyInfo[] GetProperties(BindingFlags bindingAttr)
        {
            List<IDynamicPropertyInfo> list = new List<IDynamicPropertyInfo>();

            foreach (PropertyInfo info in type.GetProperties(bindingAttr))
            {
                list.Add(new RefPropertyInfo(type, info));
            }

            return list.ToArray();
        }
    }
}
