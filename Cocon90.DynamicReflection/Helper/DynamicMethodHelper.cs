using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using Cocon90.DynamicReflection.Member;

namespace Cocon90.DynamicReflection.Helper
{
    class DynamicMethodHelper
    {


        /// <summary>
        /// 创建PropertyInfo的动态方法get
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        internal static PropertyGetDelegate CreateGetHandler(Type type, PropertyInfo propertyInfo)
        {
            MethodInfo getMethodInfo = propertyInfo.GetGetMethod(true);
            int argIndex = 0;

            DynamicMethod dynamicGet = new DynamicMethod("DynamicGet", typeof(object), new Type[] { typeof(object), typeof(object[]) }, type, true);
            ILGenerator getGenerator = dynamicGet.GetILGenerator();

            getGenerator.Emit(OpCodes.Ldarg_0);
            foreach (ParameterInfo parainfo in getMethodInfo.GetParameters())
            {
                getGenerator.Emit(OpCodes.Ldarg_1);
                if (argIndex > 8)
                    getGenerator.Emit(GetLdc_I4(argIndex), argIndex);
                else
                    getGenerator.Emit(GetLdc_I4(argIndex));
                getGenerator.Emit(OpCodes.Ldelem_Ref);
                UnboxIfNeeded(getGenerator, parainfo.ParameterType);
                argIndex++;
            }
            getGenerator.Emit(OpCodes.Callvirt, getMethodInfo);
            BoxIfNeeded(getGenerator, getMethodInfo.ReturnType);
            getGenerator.Emit(OpCodes.Ret);

            return (PropertyGetDelegate)dynamicGet.CreateDelegate(typeof(PropertyGetDelegate));
        }
        /// <summary>
        /// 创建构造函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="constructorInfo"></param>
        /// <returns></returns>
        internal static ConstructorInfoDelegate CreateDynamicConstructorInfoHandler(Type type, ConstructorInfo constructorInfo)
        {
            int argIndex = 0;
            DynamicMethod dynamicMethod = new DynamicMethod("DynamicConstructor",
                MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(object), new Type[] { typeof(object[]) }, type, true);
            ILGenerator generator = dynamicMethod.GetILGenerator();

            foreach (ParameterInfo parainfo in constructorInfo.GetParameters())
            {
                generator.Emit(OpCodes.Ldarg_0);
                if (argIndex > 8)
                    generator.Emit(GetLdc_I4(argIndex), argIndex);
                else
                    generator.Emit(GetLdc_I4(argIndex));
                generator.Emit(OpCodes.Ldelem_Ref);
                UnboxIfNeeded(generator, parainfo.ParameterType);
                argIndex++;
            }
            generator.Emit(OpCodes.Newobj, constructorInfo);
            generator.Emit(OpCodes.Ret);
            return (ConstructorInfoDelegate)dynamicMethod.CreateDelegate(typeof(ConstructorInfoDelegate));
        }

        /// <summary>
        /// 创建PropertyInfo的动态方法get
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        internal static PropertySetDelegate CreateSetHandler(Type type, PropertyInfo propertyInfo)
        {
            MethodInfo setMethodInfo = propertyInfo.GetSetMethod(true);
            int argCount = setMethodInfo.GetParameters().Length;
            int argIndex = 0;

            DynamicMethod dynamicSet = new DynamicMethod("DynamicSet", typeof(void), new Type[] { typeof(object), typeof(object), typeof(object[]) }, type, true);
            ILGenerator setGenerator = dynamicSet.GetILGenerator();

            setGenerator.Emit(OpCodes.Ldarg_0);
            foreach (ParameterInfo parainfo in setMethodInfo.GetParameters())
            {
                if (argIndex + 1 >= argCount)
                    break;

                setGenerator.Emit(OpCodes.Ldarg_2);
                if (argIndex > 8)
                    setGenerator.Emit(GetLdc_I4(argIndex), argIndex);
                else
                    setGenerator.Emit(GetLdc_I4(argIndex));
                setGenerator.Emit(OpCodes.Ldelem_Ref);
                UnboxIfNeeded(setGenerator, parainfo.ParameterType);
                argIndex++;
            }
            setGenerator.Emit(OpCodes.Ldarg_1);
            UnboxIfNeeded(setGenerator, setMethodInfo.GetParameters()[argIndex].ParameterType);
            setGenerator.Emit(OpCodes.Call, setMethodInfo);
            setGenerator.Emit(OpCodes.Ret);

            return (PropertySetDelegate)dynamicSet.CreateDelegate(typeof(PropertySetDelegate));
        }


        /// <summary>
        /// 创建Field的动态方法set
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        internal static FieldSetDelegate CreateSetHandler(Type type, FieldInfo fieldInfo)
        {
            DynamicMethod dynamicSet = CreateSetDynamicMethod(type);
            ILGenerator setGenerator = dynamicSet.GetILGenerator();

            setGenerator.Emit(OpCodes.Ldarg_0);
            setGenerator.Emit(OpCodes.Ldarg_1);
            UnboxIfNeeded(setGenerator, fieldInfo.FieldType);
            setGenerator.Emit(OpCodes.Stfld, fieldInfo);
            setGenerator.Emit(OpCodes.Ret);

            return (FieldSetDelegate)dynamicSet.CreateDelegate(typeof(FieldSetDelegate));
        }
        /// <summary>
        /// 创建Field的动态方法get
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        internal static FieldGetDelegate CreateGetHandler(Type type, FieldInfo fieldInfo)
        {
            DynamicMethod dynamicGet = CreateGetDynamicMethod(type);
            ILGenerator getGenerator = dynamicGet.GetILGenerator();

            getGenerator.Emit(OpCodes.Ldarg_0);
            getGenerator.Emit(OpCodes.Ldfld, fieldInfo);
            BoxIfNeeded(getGenerator, fieldInfo.FieldType);
            getGenerator.Emit(OpCodes.Ret);

            return (FieldGetDelegate)dynamicGet.CreateDelegate(typeof(FieldGetDelegate));
        }


        private static DynamicMethod CreateSetDynamicMethod(Type type)
        {
            return new DynamicMethod("DynamicSet", typeof(void), new Type[] { typeof(object), typeof(object) }, type, true);
        }
        private static DynamicMethod CreateGetDynamicMethod(Type type)
        {
            return new DynamicMethod("DynamicGet", typeof(object), new Type[] { typeof(object) }, type, true);
        }



        internal static OpCode GetLdc_I4(int index)
        {
            if (index == 0)
                return OpCodes.Ldc_I4_0;
            if (index == 1)
                return OpCodes.Ldc_I4_1;
            if (index == 2)
                return OpCodes.Ldc_I4_2;
            if (index == 3)
                return OpCodes.Ldc_I4_3;
            if (index == 4)
                return OpCodes.Ldc_I4_4;
            if (index == 5)
                return OpCodes.Ldc_I4_5;
            if (index == 6)
                return OpCodes.Ldc_I4_6;
            if (index == 7)
                return OpCodes.Ldc_I4_7;
            if (index == 8)
                return OpCodes.Ldc_I4_8;
            if (index > 8)
                return OpCodes.Ldc_I4_S;

            throw new Exception("unknown index to ldc_i4");
        }
        internal static void BoxIfNeeded(ILGenerator generator, Type type)
        {
            if (type.GetTypeInfo().IsValueType)
            {
                generator.Emit(OpCodes.Box, type);
            }
        }

        /// <summary>
        /// 判断是否需要拆箱
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="type"></param>
        internal static void UnboxIfNeeded(ILGenerator generator, Type type)
        {
            if (type.GetTypeInfo().IsValueType)
            {
                generator.Emit(OpCodes.Unbox_Any, type);
            }
        }
    }
}

