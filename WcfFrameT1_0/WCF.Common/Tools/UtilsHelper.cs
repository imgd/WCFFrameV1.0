using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace WCF.Common.Tools
{
    public static class UtilsHelper
    {

        #region  弹出消息
        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="msg">消息内容</param>
        public static void alert(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), null, "alert('" + msg + "');", true);
        }
        /// <summary>
        /// 刷新当前页面
        /// </summary>
        /// <param name="page"></param>
        public static void Reload(Page page)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), null, "window.location.href='" + page.Request.Url.AbsoluteUri + "';", true);
        }
        /// <summary>
        /// 弹出消息框,并刷新当前页面
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="msg">消息内容</param>
        public static void alertMsg(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), null, "alert('" + msg + "');window.location.href='" + page.Request.Url.AbsoluteUri + "';", true);
        }
        /// <summary>
        /// 弹出消息框,并刷新当前页面 包含传递参数（暂时在商家商品添加时用）
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void alertRealPro(Page page, string msg)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), null, "alert('" + msg + "');window.location.href='" + page.Request.Url.AbsoluteUri + "';", true);
        }
        /// <summary>
        /// 弹出消息框 自定义消息
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="msg">消息内容</param>
        public static void alertSys(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), null, msg + "window.location.href='" + page.Request.Url.AbsoluteUri + "';", true);
        }
        /// <summary>
        /// 弹出消息框 自定义消息 跳转到指定页面
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="msg">消息内容</param>
        public static void alertSys(Page page, string msg, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), null, msg + "window.location.href='" + url + "';", true);
        }
        /// <summary>
        /// 弹出消息框,并跳转页面
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="msg">消息内容</param>
        /// <param name="msg">指定跳转路径</param>
        public static void alertMsg(Page page, string msg, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), null, "alert('" + msg + "');window.location.href='" + url + "';", true);
        }
        #endregion

        /// <summary>
        /// 将表示秒的字符串装换为表示分：秒的字符串
        /// </summary>
        /// <returns>返回00::00格式时间字符串</returns>
        public static string SecondStringToTime(string Str)
        {
            string timeStr = "";
            int i = 0;
            if (int.TryParse(Str, out i))
            {
                int timeInt = int.Parse(Str);
                int minute = timeInt / 60;
                int second = timeInt % 60;
                string minuteStr = minute >= 10 ? minute.ToString() : "0" + minute.ToString();
                string secondStr = second >= 10 ? second.ToString() : "0" + second.ToString();
                timeStr = minuteStr + ":" + secondStr;
            }
            return timeStr;
        }

        /// <summary>
        /// 添加网页标题
        /// </summary>
        /// <param name="page"></param>
        /// <param name="title"></param>
        /// <param name="keywords"></param>
        /// <param name="description"></param>
        public static void SetHeader(Page page, string title, string keywords, string description)
        {
            page.Title = title;
            page.MetaKeywords = keywords;
            page.MetaDescription = description;
        }

        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        public static string Cre_OrerNumBer()
        {
            int count = 2;//随机数位数
            string orderinfo = "";
            string[] source = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string code = "";
            Random rd = new Random();
            for (int i = 0; i < count; i++)
            {
                code += source[rd.Next(0, source.Length)];
            }
            DateTime Dt = DateTime.Now;
            orderinfo = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000 + code;
            return orderinfo;
        }
    }
}
