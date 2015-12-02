using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace WCF.Common.Lib
{
    public class RequestHelper
    {

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
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
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }
        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        /// <summary>
        /// 返回指定的服务器变量信息
        /// </summary>
        /// <param name="strName">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        public static string GetServerString(string strName)
        {
            if (HttpContext.Current.Request.ServerVariables[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.ServerVariables[strName].ToString();
        }

        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;

            try
            {
                retVal = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch { }

            if (retVal == null)
                return "";

            return retVal;

        }

        /// <summary>
        /// 得到当前完整主机头
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost()
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            if (!request.Url.IsDefaultPort)
            {
                return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
            }
            return request.Url.Host;
        }

        /// <summary>
        /// 得到主机头
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }

        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
        /// </summary>
        /// <returns>原始 URL</returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string IPAddress()
        {

            string result = System.String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == System.String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == System.String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;

        }
        /// <summary>
        /// 取得客户端真实IP。如果有代理则取第一个非内网地址   
        /// </summary>
        public static string GetIP()
        {

            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result != null && result != String.Empty)
            {
                //可能有代理
                if (result.IndexOf(".") == -1) //没有“.”肯定是非IPv4格式
                    result = null;
                else
                {
                    if (result.IndexOf(",") != -1)
                    {
                        //有“,”，估计多个代理。取第一个不是内网的IP。
                        result = result.Replace(" ", "").Replace("'", "");
                        string[] temparyip = result.Split(",;".ToCharArray());
                        for (int i = 0; i < temparyip.Length; i++)
                        {
                            if (IsIPAddress(temparyip[i])
                            && temparyip[i].Substring(0, 3) != "10."
                            && temparyip[i].Substring(0, 7) != "192.168"
                            && temparyip[i].Substring(0, 7) != "172.16.")
                            {
                                return temparyip[i]; //找到不是内网的地址
                            }
                        }
                    }
                    else if (IsIPAddress(result)) //代理即是IP格式
                        return result;
                    else
                        result = null; //代理中的内容 非IP，取IP
                }
            }
            if (null == result || result == String.Empty)
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (result == null || result == String.Empty)
                result = HttpContext.Current.Request.UserHostAddress;
            return result;

        }
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        public static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;
            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";
            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }

        /// <summary>
        /// 获取url地址参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="defaultnum">默认值</param>
        /// <returns></returns>
        public static int GetUrlInt(string name, int defaultnum)
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
        public static int GetUrlInt(string name)
        {
            return GetUrlInt(name, 0);
        }

        public static byte GetUrlByte(string paramName, byte defaultVal)
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
        public static int GetFormInt(string name, int defaultnum)
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
        public static int GetFormInt(string name)
        {
            return GetFormInt(name, 0);
        }
        /// <summary>
        /// 获取url地址参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="defaultnum">默认值</param>
        /// <returns></returns>
        public static string GetUrlString(string name, string defaultnum)
        {
            string num = defaultnum;
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
                    return str;
                }
            }
        }
        /// <summary>
        /// 获取url地址参数  默认值是0
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns></returns>
        public static string GetUrlString(string name)
        {
            return GetUrlString(name, "0");
        }
        /// <summary>
        ///  获取Form地址参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="defaultnum">默认值</param>
        /// <returns></returns>
        public static string GetFormString(string name, string defaultnum)
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
        public static string GetFormString(string name)
        {
            return GetFormString(name, "0");
        }
        /// <summary>
        /// 检测字符串是否全是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNum(String str)
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
        public static List<string> GetUrlAllPara(string paraUrl)
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


        /// <summary>
        /// 新浪查询ip所在的接口提供的model
        /// </summary>
        public class IpDetail
        {
            /// <summary>
            ///  查询ip信息返回的 数据状态，-1为没有查到如192.168.0.1，1查到了ip信息
            /// </summary>
            public String Ret { get; set; }
            /// <summary>
            /// ip范围 开始ip
            /// </summary>
            public String Start { get; set; }
            /// <summary>
            /// ip范围 结束的ip
            /// </summary>
            public String End { get; set; }
            /// <summary>
            /// 国家 
            /// </summary>
            public String Country { get; set; }

            /// <summary>
            /// 省
            /// </summary>
            public String Province { get; set; }
            /// <summary>
            /// 市
            /// </summary>
            public String City { get; set; }
            /// <summary>
            /// 区
            /// </summary>
            public String District { get; set; }
            /// <summary>
            /// 服务供应商
            /// </summary>
            public String Isp { get; set; }

            public String Type { get; set; }

            public String Desc { get; set; }

        }
    }
}
