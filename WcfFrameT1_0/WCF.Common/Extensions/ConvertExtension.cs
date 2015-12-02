using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensions.ConvertExtension
{
    //---------------------------------------------------------------
    //   类型转换判断类                                                   
    // —————————————————————————————————————————————————             
    // | varsion 1.0                                   |             
    // | creat by gd 2014.7.31                         |             
    // | 联系我:@大白2013 http://weibo.com/u/2239977692 |            
    // —————————————————————————————————————————————————             
    //                                                               
    // *使用说明：                                                    
    //    使用当前扩展类添加引用: using Extensions.ConvertExtension;                      
    //    使用所有扩展类添加引用: using Extensions;                         
    // --------------------------------------------------------------

    public static class ConvertExtension
    {

        /// <summary>
        /// 判断对象是否是Null
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// 是否是int型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsInt(this object val)
        {
            int i;
            return int.TryParse(val.ToString(), out i);
        }
        /// <summary>
        /// 是否是datetime型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsDateTime(this object val)
        {
            DateTime t;
            return DateTime.TryParse(val.ToString(), out t);
        }
        /// <summary>
        /// 是否是byte型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsByte(this object val)
        {
            byte b;
            return byte.TryParse(val.ToString(), out b);
        }

        /// <summary>
        /// 是否是float型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsFloat(this object val)
        {
            float f;
            return float.TryParse(val.ToString(), out f);
        }

        /// <summary>
        /// 是否是double型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsDouble(this object val)
        {
            double d;
            return double.TryParse(val.ToString(), out d);
        }

        /// <summary>
        /// 是否是decimal型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsDecimal(this object val)
        {
            decimal d;
            return decimal.TryParse(val.ToString(), out d);
        }

        /// <summary>
        /// 返回 object值 转换成int值
        /// </summary>
        /// <param name="val">转换值</param>
        /// <returns></returns>
        public static int ParseInt(this object val)
        {
            //使用情况说明
            //转换对象可能存在文本字符串转int 
            if (val == null)
                return 0;

            int result = 0;
            int.TryParse(val.ToString().Trim(), out result);
            return result;
        }


        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsNullOrEnpty(this string path)
        {
            return string.IsNullOrEmpty(path);
        }

    }
}
