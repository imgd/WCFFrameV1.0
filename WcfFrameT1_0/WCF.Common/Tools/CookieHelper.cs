using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WCF.Common;

namespace WCF.Common.Tools
{
    public class CookieHelper
    {
        #region Cookie操作
        /// <summary>
        /// 取Cookie
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <returns></returns>
        public static string GetCookie(string cookieName)
        {
            return GetCookie(cookieName, "");
        }
        /// <summary>
        /// 取Cookie
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strDefaultValue">当没有值的时候的默认值</param>
        /// <returns></returns>
        public static string GetCookie(string cookieName, string defaultValue)
        {
            cookieName = cookieName.ToLower();
            string strResult = defaultValue;
            try
            {
                if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies["xm"] != null && HttpContext.Current.Request.Cookies["xm"][cookieName] != null)
                {
                    strResult = HttpContext.Current.Request.Cookies["xm"][cookieName].ToString();
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
            return strResult;
        }


        /// <summary>
        /// 保存Cookie
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <param name="cookieValue">值</param>
        public static void SetCookie(string cookieName, string cookieValue)
        {
            SetCookie(cookieName, cookieValue, 60);
        }
        /// <summary>
        /// 保存Cookie       
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <param name="cookieValue">值</param>
        /// <param name="cookieExpiryMinute">到期时间</param>
        public static void SetCookie(string cookieName, string cookieValue, int cookieExpiryMinute)
        {
            cookieName = cookieName.ToLower();
            HttpCookie cookie = HttpContext.Current.Request.Cookies["xm"];

            if (cookie == null)
            {
                cookie = new HttpCookie("xm");
                cookie.Values[cookieName] = HttpUtility.UrlEncode(cookieValue);
            }
            else
            {
                cookie.Values[cookieName] = HttpUtility.UrlEncode(cookieValue);
            }
            //HttpCookie cookie = new HttpCookie(cookieName, cookieValue);
            cookie.Domain = ConfigHelper.GetConfigConnnString("cookieDomain"); 
            HttpContext.Current.Response.Cookies.Remove(cookieName);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        
        /// <summary>
        /// 写入Cookies值 dnt 
        /// </summary>
        /// <param name="strName">项</param>
        /// <param name="strValue">值</param>
        /// <param name="cookieExpiryMinute">到期时间</param>
        public static void BbsWriteCookies(string strName, string strValue)
        {
            int cookieExpiryMinute = 60;
            HttpCookie cookie = HttpContext.Current.Request.Cookies["dnt"];
            if (cookie == null)
            {
                cookie = new HttpCookie("dnt");
                cookie.Values[strName] = HttpUtility.UrlEncode(strValue);
            }
            else
            {
                cookie.Values[strName] = HttpUtility.UrlEncode(strValue);
            }
            //cookie.Expires = DateTime.Now.AddDays(cookieExpiryMinute);//设置Cookie过期时间
            cookie.Domain = ConfigHelper.GetConfigConnnString("cookieDomain"); 
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// 清除Cookie
        /// </summary>
        /// <param name="cookieName">名称</param>
        public static void ClearCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["xm"];
            if (cookie == null)
            {
                cookie = new HttpCookie("xm");
            }

            cookie.Expires = DateTime.Now.AddDays(-365);
            cookie.Domain = ConfigHelper.GetConfigConnnString("cookieDomain"); 
            HttpContext.Current.Response.Cookies.Remove(cookieName);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #endregion

        #region LoginCookie操作
        /// <summary>
        /// 保存LoginCookie       
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <param name="cookieValue">值</param>
        /// <param name="cookieExpiryMinute">到期时间</param>
        public static void SetLoginCookie(string cookieName, string cookieValue, int cookieExpiryMinute)
        {
            cookieName = cookieName.ToLower();
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookieName);
                cookie.Value = HttpUtility.UrlEncode(cookieValue);
            }
            else
            {
                cookie.Value = HttpUtility.UrlEncode(cookieValue);
            }
            if (cookieExpiryMinute > 0)
            {
                cookie.Expires = DateTime.Now.AddDays(cookieExpiryMinute);//设置Cookie过期时间
            }
            cookie.Domain = ConfigHelper.GetConfigConnnString("cookieDomain"); 
            //删除旧的同名Cookie
            HttpContext.Current.Response.Cookies.Remove(cookieName);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// 保存LoginCookie 关闭浏览器cookie实现
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <param name="cookieValue">值</param>
        public static void SetLoginCookie(string cookieName, string cookieValue)
        {
            SetLoginCookie(cookieName, cookieValue, 0);
        }
        /// <summary>
        /// 取LoginCookie
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strDefaultValue">当没有值的时候的默认值</param>
        /// <returns></returns>
        public static string GetLoginCookie(string cookieName, string defaultValue)
        {
            cookieName = cookieName.ToLower();
            string strResult = defaultValue;
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                strResult = HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[cookieName].Value);
            }
            return strResult;
        }
        /// <summary>
        /// 取LoginCookie
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strDefaultValue">当没有值的时候的默认值</param>
        /// <returns></returns>
        public static string GetLoginCookie(string cookieName)
        {
            return GetLoginCookie(cookieName, "");
        }
        /// <summary>
        /// 清除LoginCookie
        /// </summary>
        /// <param name="cookieName">名称</param>
        public static void ClearLoginCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-365);
                cookie.Domain = ConfigHelper.GetConfigConnnString("cookieDomain"); 
                HttpContext.Current.Response.Cookies.Remove(cookieName);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        #endregion


        #region 单独获取cookie操作
        /// <summary>
        /// 获取单独Cookie的值
        /// </summary>
        /// <param name="strName">Cookie的名称</param>
        /// <param name="strDefaultValue">当没有值的时候的默认值</param>
        /// <returns></returns>
        public static string GetSingleCookie(string cookieName, string defaultValue)
        {
            cookieName = cookieName.ToLower();
            string strResult = defaultValue;
            try
            {
                if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[cookieName] != null && HttpContext.Current.Request.Cookies[cookieName].Value != null)
                {
                    strResult = HttpContext.Current.Request.Cookies[cookieName].Value.ToString();
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
            return strResult;
        }

        /// <summary>
        /// 获取单独Cookie的值
        /// </summary>
        /// <param name="strName">Cookie的名称</param>
        /// <param name="strDefaultValue">当没有值的时候的默认值</param>
        /// <returns></returns>
        public static string GetSingleCookie(string cookieName)
        {
            return GetSingleCookie(cookieName, "");
        }


        /// <summary>
        /// 保存Cookie        
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <param name="cookieValue">值</param>
        /// <param name="cookieExpiryMinute">该cookie多少分钟后过期</param>
        public static void SetSingleCookie(string cookieName, string cookieValue, int cookieExpiryMinute)
        {
            cookieName = cookieName.ToLower();
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];

            if (cookie == null)
            {
                cookie = new HttpCookie(cookieName);
                cookie.Value = cookieValue;
            }
            else
            {
                cookie.Value = cookieValue;
            }
            //HttpCookie cookie = new HttpCookie(cookieName, cookieValue);
            cookie.Domain = ConfigHelper.GetConfigConnnString("cookieDomain"); 
            if (cookieExpiryMinute > 0)
                cookie.Expires = DateTime.Now.AddMinutes(cookieExpiryMinute);
            HttpContext.Current.Response.Cookies.Remove(cookieName);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 保存Cookie
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <param name="cookieValue">值</param>
        public static void SetSingleCookie(string cookieName, string cookieValue)
        {
            SetSingleCookie(cookieName, cookieValue, 60);
        }

        #endregion

    }
}
