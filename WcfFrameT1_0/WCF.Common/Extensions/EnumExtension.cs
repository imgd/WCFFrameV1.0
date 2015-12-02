using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensions.EnumExtension
{
    //---------------------------------------------------------------
    //   Enum 扩展方法类                                                   
    // —————————————————————————————————————————————————             
    // | varsion 1.0                                   |             
    // | creat by gd 2014.7.31                         |             
    // | 联系我:@大白2013 http://weibo.com/u/2239977692 |            
    // —————————————————————————————————————————————————             
    //                                                               
    // *使用说明：                                                    
    //    使用当前扩展类添加引用: using Extensions.EnumExtension;                      
    //    使用所有扩展类添加引用: using Extensions;                         
    // -------------------------------------------------------------- 
    public static class EnumExtension
    {
        /// <summary>
        /// 返回枚举的名称
        /// </summary>
        /// <typeparam name="T">枚举值类型</typeparam>
        /// <param name="key">枚举值</param>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static string GetEnumName(this object val, Type enumType)
        {
            return Enum.GetName(enumType, val);
        }
        /// <summary>
        /// 获取枚举的键值对
        /// </summary>        
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetEnumVals(this Type enumType)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (string val in enumType.GetEnumValues())
            {
                dic.Add(GetEnumName(val, enumType), val);
            }
            return dic;
        }

        /// <summary>
        /// 判断枚举值是否包含
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="type">枚举类型</param>
        /// <returns></returns>
        public static bool IsEnumContainsValue(this object value, Type type)
        {
            return value.IsInArray(type.GetEnumValues());
        }
    }
}
