using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Cocon90.DynamicReflection
{
    public interface IDynamicType
    {
        IDynamicConstructorInfo GetConstructor(Type[] types);

        IDynamicConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);
        IDynamicConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);
        IDynamicConstructorInfo[] GetConstructors();
        IDynamicConstructorInfo[] GetConstructors(BindingFlags bindingAttr);


        IDynamicFieldInfo GetField(string name);
        IDynamicFieldInfo GetField(string name, BindingFlags bindingAttr);
        IDynamicFieldInfo[] GetFields();
        IDynamicFieldInfo[] GetFields(BindingFlags bindingAttr);


        IDynamicPropertyInfo GetProperty(string name);
        IDynamicPropertyInfo GetProperty(string name, BindingFlags bindingAttr);
        IDynamicPropertyInfo GetProperty(string name, Type returnType);
        IDynamicPropertyInfo GetProperty(string name, Type[] types);
        IDynamicPropertyInfo GetProperty(string name, Type returnType, Type[] types);

        IDynamicPropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers);
        IDynamicPropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);
        IDynamicPropertyInfo[] GetProperties();
        IDynamicPropertyInfo[] GetProperties(BindingFlags bindingAttr);
    }
}
