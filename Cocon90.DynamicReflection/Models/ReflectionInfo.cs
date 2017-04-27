using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cocon90.DynamicReflection.Models
{
    public class ReflectionInfo
    {
        public ReflectionInfo(Type innerType)
        {
            this.InnerType = innerType;
            this.DynamicType = new DynamicType(innerType);
            this.DynamicConstructorInfo = DynamicType.GetConstructor(new Type[0]);
            this.Properties = DynamicType.GetProperties();
            this.DynamicPropertyDics = new Dictionary<string, IDynamicPropertyInfo>();
            foreach (var pro in Properties)
            {
                DynamicPropertyDics.Add(pro.Name, pro);
            }

        }
        /// <summary>
        /// 原始类型
        /// </summary>
        public Type InnerType { get; set; }
        /// <summary>
        /// 动态类型
        /// </summary>
        public DynamicType DynamicType { get; set; }
        /// <summary>
        /// 默认的无参构造函数 
        /// </summary>
        public IDynamicConstructorInfo DynamicConstructorInfo { get; set; }
        /// <summary>
        /// 所有动态属性。
        /// </summary>
        public IDynamicPropertyInfo[] Properties { get; set; }
        /// <summary>
        /// 属性名称为Key，值为动态属性的字典。
        /// </summary>
        public Dictionary<string, IDynamicPropertyInfo> DynamicPropertyDics { get; set; }

    }
}
