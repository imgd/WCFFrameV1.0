using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Extensions.JsonExtension
{
    //---------------------------------------------------------------
    //   Json扩展方法类                                                   
    // —————————————————————————————————————————————————             
    // | varsion 1.0                                   |             
    // | creat by gd 2014.7.31                         |             
    // | 联系我:@大白2013 http://weibo.com/u/2239977692 |            
    // —————————————————————————————————————————————————             
    //                                                               
    // *使用说明：                                                    
    //    使用当前扩展类添加引用: using Extensions.JsonExtension;                      
    //    使用所有扩展类添加引用: using Extensions;                         
    // -------------------------------------------------------------- 

    public static class JsonExtension
    {
        #region JSON (反)序列化

        /// <summary>
        /// 返回对象的序列化成JSON字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string DocumentsToJson(this object obj)
        {
            if (obj == null)
                return string.Empty;

            return (new JavaScriptSerializer()).Serialize(obj);
        }
        /// <summary>
        /// 返回对象的序列化成JSON字符串 
        /// </summary>
        /// <param name="obj"></param>         
        /// <returns></returns>
        public static string DocumentsToJson(this object obj,bool isdatacontract)
        {
            if (obj == null)
                return string.Empty;

            string result = string.Empty;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                ser.WriteObject(stream, obj);
                stream.Position = 0;
                StreamReader reader = new StreamReader(stream);
                result = reader.ReadToEnd();
            }
            return result;
        }
        

        /// <summary>
        /// 返回JSON字符串反序列化成泛型对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T JsonToDocument<T>(this string json) where T : new()
        {
            if (json == null)
                return new T();

            return (new JavaScriptSerializer()).Deserialize<T>(json);
        }
        /// <summary>
        /// 返回JSON字符串反序列化成泛型对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T JsonToDocument<T>(this string json, bool isdatacontract) where T : class
        {
            if (json == null)
                return null;

            T result = null;
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                result = ser.ReadObject(stream) as T;
            }
            return result;
        }

        /// <summary>
        /// 返回JSON字符串反序列化成List泛型对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<T> JsonToDocuments<T>(this string json)
        {
            if (json == null)
                return new List<T>();

            return (new JavaScriptSerializer()).Deserialize<List<T>>(json);
        }

        /// <summary>
        /// 返回JSON字符串反序列化成Dic字典对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, object> JsonToDocuments(this string json)
        {
            if (json == null)
                return new Dictionary<string, object>();

            return (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(json);
        }


        


        #endregion
    }
}
