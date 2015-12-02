using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace Extensions.WebRequestExtension
{
    //---------------------------------------------------------------
    //   web请求参数获取 扩展方法类  
    //   此扩展适用于公开服务的几口参数验证                                            
    // —————————————————————————————————————————————————             
    // | varsion 1.0                                   |             
    // | creat by gd 2014.7.31                         |             
    // | 联系我:@大白2013 http://weibo.com/u/2239977692 |            
    // —————————————————————————————————————————————————             
    //                                                               
    // *使用说明：                                                    
    //    使用当前扩展类添加引用: using Extensions.WebRequestExtension;                      
    //    使用所有扩展类添加引用: using Extensions;                         
    // --------------------------------------------------------------
    public static class WebRequestExtension
    {

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(this string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        

        /// <summary>
        /// 返回指定的服务器变量信息
        /// </summary>
        /// <param name="strName">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        public static string GetServerString(this string strName)
        {
            if (HttpContext.Current.Request.ServerVariables[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.ServerVariables[strName].ToString();
        }

        /// <summary>
        /// 获取url地址参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="defaultnum">默认值</param>
        /// <returns></returns>
        public static int GetUrlInt(this string name, int defaultnum)
        {
            int num = defaultnum;
            if (null == HttpContext.Current.Request.QueryString[name])
            {
                return defaultnum;
            }
            else
            {
                string str = HttpContext.Current.Request.QueryString[name].Trim();
                if (str.Length < 1)
                {
                    return defaultnum;
                }
                else
                {
                    if (int.TryParse(str, out num))
                    {
                        return num;
                    }
                    else
                    {
                        return defaultnum;
                    }
                }
            }
        }
        /// <summary>
        /// 获取url地址参数  默认值是0
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns></returns>
        public static int GetUrlInt(this string name)
        {
            return GetUrlInt(name, 0);
        }

        public static byte GetUrlByte(this string paramName, byte defaultVal)
        {
            string value = HttpContext.Current.Request[paramName];
            if (value == null || value.Length <= 0)
            {
                return defaultVal;
            }
            byte valueInt = defaultVal;
            byte.TryParse(value, out valueInt);

            return valueInt;
        }

        /// <summary>
        /// 获取Form参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="defaultnum">默认值</param>
        /// <returns></returns>
        public static int GetFormInt(this string name, int defaultnum)
        {
            int num = defaultnum;
            if (null == HttpContext.Current.Request.Form[name])
            {
                return defaultnum;
            }
            else
            {
                string str = HttpContext.Current.Request.Form[name].Trim();
                if (str.Length < 1)
                {
                    return defaultnum;
                }
                else
                {
                    if (int.TryParse(str, out num))
                    {
                        return num;
                    }
                    else
                    {
                        return defaultnum;
                    }
                }
            }
        }
        /// <summary>
        /// 获取Form参数  默认值是0
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns></returns>
        public static int GetFormInt(this string name)
        {
            return GetFormInt(name, 0);
        }
        /// <summary>
        /// 获取url地址参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="defaultnum">默认值</param>
        /// <returns></returns>
        public static string GetUrlString(this string name, string defaultstr)
        {
            string num = defaultstr;
            if (null == HttpContext.Current.Request.QueryString[name])
            {
                return defaultstr;
            }
            else
            {
                string str = HttpContext.Current.Request.QueryString[name].Trim();
                if (str.Length < 1)
                {
                    return defaultstr;
                }
                else
                {
                    return str;
                }
            }
        }
        /// <summary>
        /// 获取url地址参数  默认值是0
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns></returns>
        public static string GetUrlString(this string name)
        {
            return GetUrlString(name, "");
        }
        /// <summary>
        ///  获取Form地址参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="defaultnum">默认值</param>
        /// <returns></returns>
        public static string GetFormString(this string name, string defaultnum)
        {
            string num = defaultnum;
            if (null == HttpContext.Current.Request.Form[name])
            {
                return defaultnum;
            }
            else
            {
                string str = HttpContext.Current.Request.Form[name].Trim();
                if (str.Length < 1)
                {
                    return defaultnum;
                }
                else
                {
                    return str;
                }
            }
        }
        /// <summary>
        ///  获取Form地址参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="defaultnum">默认值</param>
        /// <returns></returns>
        public static string GetFormString(this string name)
        {
            return GetFormString(name, "");
        }
        /// <summary>
        /// 检测字符串是否全是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNum(this String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsNumber(str, i))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 获取url参数部分数据
        /// </summary>
        /// <param name="paraUrl">url参数</param>
        /// <returns></returns>
        public static List<string> GetUrlAllPara(this string paraUrl)
        {
            List<string> para = new List<string>();
            if (paraUrl != "")
            {
                string[] arrc = paraUrl.Trim().Split('&');
                if (arrc.Length > 0)
                {
                    foreach (string item in arrc)
                    {
                        para.Add(item.Trim());
                    }
                }
            }
            return para;
        }
    }
}
