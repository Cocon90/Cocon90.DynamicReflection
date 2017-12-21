using Cocon90.DynamicReflection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cocon90.DynamicReflection
{
    /// <summary>
    /// 动态类型调用并提供缓冲
    /// </summary>
    public class ReflectionCacheManager
    {
        private volatile Dictionary<string, ReflectionInfo> dictionary = new Dictionary<string, ReflectionInfo>();
        private readonly object lockObj = new object();
        public static ReflectionCacheManager Instance { get { if (_Instance == null) _Instance = new ReflectionCacheManager(); return _Instance; } }
        private static ReflectionCacheManager _Instance = null;
        /// <summary>
        /// 从缓冲中获取所需要类型
        /// </summary>
        public ReflectionInfo Get(Type type)
        {
            return Get(type.FullName);
        }
        /// <summary>
        /// 通过的FullName属性从缓冲中获取所需要类型
        /// </summary>
        public ReflectionInfo Get(string typeFullName)
        {
            lock (lockObj)
            {
                if (dictionary.ContainsKey(typeFullName)) return dictionary[typeFullName];
                return null;
            }
        }
        /// <summary>
        /// 将指定类型，与值信息，加入到缓冲。
        /// </summary>
        public ReflectionInfo Set(Type type)
        {
            return Set(type.FullName, new ReflectionInfo(type));
        }
        /// <summary>
        /// 将指定类型FullName属性，与值信息，加入到缓冲。
        /// </summary>
        public ReflectionInfo Set(string typeFullName, ReflectionInfo value)
        {
            lock (lockObj)
            {
                dictionary[typeFullName] = value;
                return value;
            }
        }
        /// <summary>
        /// 获取指定类型的反射信息，如果没有，将生成并返回。
        /// </summary>
        public ReflectionInfo GetAndSet(Type type)
        {
            lock (lockObj)
            {
                if (dictionary.ContainsKey(type.FullName))
                    return dictionary[type.FullName];
                return Set(type);
            }
        }
         
    }
}
