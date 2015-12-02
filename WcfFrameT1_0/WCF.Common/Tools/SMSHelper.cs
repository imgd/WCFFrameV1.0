using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Threading;
using Extensions.ConvertExtension;
using Extensions.VerificationExtension;
using Extensions.StringExtension;

namespace WCF.Common.Tools
{
    /// <summary>
    /// CD 凌凯 短信接口
    /// </summary>
    public static class SMSHelper
    {

        private static Encoding myEncoding = Encoding.GetEncoding("gb2312");
        private static string URL_SEND_SMS = "http://125.69.81.40:83/wsn/BatchSend.aspx";
        private static string URL_GET_YE = "http://125.69.81.40:83/wsn/SelSum.aspx";
        private static string SMSKey = HttpUtility.UrlEncode(
            ConfigHelper.GetAppSettingsString("SMSUserKey"), myEncoding);
        private static string SMSPwd = HttpUtility.UrlEncode(
            ConfigHelper.GetAppSettingsString("SMSUserPWD"), myEncoding);


        /// <summary>
        /// 发送短信异步 单发
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SendSMS_ASYN(string mobile, string content)
        {
            if (!mobile.MobileVerify() || content.Length <= 0)
            {
                return "fail";
            }

            SMSModel m = new SMSModel
            {
                mobile = mobile,
                sendContents = content
            };

            ThreadPool.QueueUserWorkItem(new WaitCallback(SendInfo), m);
            return "ok";
        }
        /// <summary>
        /// 发送短信异步 群发
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SendSMS_ASYN(List<string> mobile, string content)
        {            
            SMSModel m = new SMSModel
            {
                mobile = mobile.ListToString(),
                sendContents = content
            };

            ThreadPool.QueueUserWorkItem(new WaitCallback(SendInfo), m);
            return "ok";
        }
        /// <summary>
        /// 发送短信同步
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SendSMS(string mobile, string content)
        {
            if (!mobile.MobileVerify() || content.Length <= 0)
            {
                return "fail";
            }

            SMSModel m = new SMSModel
            {
                mobile = mobile,
                sendContents = content
            };

            return SendInfo_t(m);
        }
        /// <summary>
        /// 发送短信同步
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SendSMS(List<string> mobile, string content)
        {
            SMSModel m = new SMSModel
            {
                mobile = mobile.ListToString(),
                sendContents = content
            };

            return SendInfo_t(m);
        }

        /// <summary>
        /// 获取短信条数
        /// </summary>
        /// <returns></returns>
        public static int GetMSM_YECount()
        {
            string param = "?";
            param += "CorpID=" + SMSKey;
            param += "&Pwd=" + SMSPwd;
            int count = RequestUrl(URL_GET_YE, param).ParseInt();
            return count;
        }



        #region PRIVATE
        /// <summary>
        /// 短信发送函数 LK WEBSERVICE 接口
        /// 
        /// 此接口支持短信群发
        /// 多个英文逗号','隔开 
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <param name="content">短信类容</param>
        /// <returns></returns>
        private static void SendInfo(object o)
        {
            SMSModel m = o as SMSModel;

            string param = "?";
            param += "CorpID=" + SMSKey;
            param += "&Pwd=" + SMSPwd;
            param += "&Mobile=" + m.mobile;
            param += "&Content=" + HttpUtility.UrlEncode(m.sendContents, myEncoding);
            param += "&Cell=";
            param += "&SendTime=";

            RequestUrl(URL_SEND_SMS, param);
        }
        private static string SendInfo_t(SMSModel m)
        {
            string param = "?";
            param += "CorpID=" + SMSKey;
            param += "&Pwd=" + SMSPwd;
            param += "&Mobile=" + m.mobile;
            param += "&Content=" + HttpUtility.UrlEncode(m.sendContents, myEncoding);
            param += "&Cell=";
            param += "&SendTime=";

            string ct = RequestUrl(URL_SEND_SMS, param);
            if (ct.Trim() == "1")
            {
                return "ok";
            }
            else
            {

                return "fail";
            }
        }

        /// <summary>
        /// URL Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private static string RequestUrl(string url, string param)
        {
            try
            {
                byte[] postBytes = Encoding.ASCII.GetBytes(param);
                HttpWebRequest Rst = (HttpWebRequest)HttpWebRequest.Create(url + param);
                HttpWebResponse Rsp = (HttpWebResponse)Rst.GetResponse();
                StreamReader reader = new StreamReader(Rsp.GetResponseStream(), myEncoding);
                string ct = reader.ReadToEnd();
                return ct;
            }
            catch (System.Net.WebException WebExcp)
            {
                return WebExcp.Message;
            }
        }
        //短信模版
        private class SMSModel
        {
            public string mobile { get; set; }
            public string sendContents { get; set; }
        }
        #endregion
    }
}
