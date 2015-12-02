using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net.Mail;
using Extensions.VerificationExtension;

namespace WCF.Common.Tools
{
    public static class EmailHelper
    {
        #region 发送邮件的方法

        /// <summary>
        /// 邮件地址
        /// </summary>
        public static string emailaddress { get; set; }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public static string emailcontent { get; set; }
        /// <summary>
        /// 邮件主题
        /// </summary>
        public static string emailsubject { get; set; }


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="emailaddress">邮件接收者地址</param>
        /// <param name="mailcontent">邮件主体内容</param>
        /// <param name="mailsubject">邮件主题  </param>
        /// <returns>错误信息</returns>
        public static void SendMail(string email, string content, string subject)
        {
            if (email.EmailVerify())
            {
                emailaddress = email;
                emailcontent = content;
                emailsubject = subject;
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc));
            }
        }

        /// <summary>
        /// 构建异步调用方法
        /// </summary>
        /// <param name="o"></param>
        private static void ThreadProc(object o)
        {
            string Mailuser = ConfigHelper.GetConfigConnnString("serverUser");
            string EmailPwd = ConfigHelper.GetConfigConnnString("serverPwd");
            string Loginuser = ConfigHelper.GetConfigConnnString("strFrom");
            string EmailHost = ConfigHelper.GetConfigConnnString("smtpHost");
            MailMessage objMailMessage = new MailMessage();

            objMailMessage.From = new MailAddress(Mailuser, "", System.Text.Encoding.UTF8);
            objMailMessage.To.Add(new MailAddress(emailaddress));
            objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            objMailMessage.Subject = emailsubject;
            objMailMessage.Body = emailcontent;
            objMailMessage.IsBodyHtml = true;
            SmtpClient objSmtpClient = new SmtpClient(EmailHost);
            objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            objSmtpClient.Credentials = new System.Net.NetworkCredential(Loginuser, EmailPwd);
            try
            {
                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
