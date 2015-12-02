using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Extensions.WebRequestExtension;
using Extensions.StringExtension;
using Extensions.ConvertExtension;
using Extensions.JsonExtension;
using System.Text;
using System.Net;
using WCF.Inspector;

namespace WCF.Service.handler.tool
{
    /// <summary>
    /// WcfTestTool 的摘要说明
    /// </summary>
    public class WcfTestTool : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string f = "f".GetUrlString();
            switch (f)
            {
                case "ajax_post":
                    Ajax_Post(context);
                    break;
                case "ajax_des_endes":
                    Ajax_DES_EnsDsScript(context);
                    break;
                case "ajax_getclienttoken":
                    Ajax_GetClientToken(context);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取客户端token
        /// </summary>
        /// <param name="context"></param>
        private void Ajax_GetClientToken(HttpContext context)
        {
            string clientKey = "clientkey".GetUrlString();
            string token = new ClientTokens(
                ClientIdentityKey.GetClientKeys(),
                ClientIdentityKey.GetClientTokenKeys()
                ).KeyEnCode(clientKey);
            context.Response.Write(new
            {
                code = 200,
                msg = "^_^ Request Success !",
                data = token
            }.DocumentsToJson());
        }
        /// <summary>
        /// DES 加/解密
        /// </summary>
        /// <param name="context"></param>
        private void Ajax_DES_EnsDsScript(HttpContext context)
        {
            string enscriptStr = "str".GetUrlString();
            //0加密 1解密  公钥见配置文件
            int enscriptType = "type".GetUrlInt(0);
            string data = enscriptType == 0 ? enscriptStr.DESEncrypt() : enscriptStr.DESDecrypt();
            context.Response.Write(new
            {
                code = 200,
                msg = "^_^ Request Success !",
                data = data
            }.DocumentsToJson());
        }

        private void Ajax_Post(HttpContext context)
        {
            try
            {
                string getparas = "para".GetUrlString();
                string geturl = "url".GetUrlString();
                int gettype = "type".GetUrlInt(0);
                string token = "token".GetUrlString(string.Empty);

                byte[] postData = Encoding.UTF8.GetBytes(getparas);
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Content-Type", "application/Json");
                if (!string.IsNullOrEmpty(token))
                {
                    webClient.Headers.Add("token", token);
                }
                byte[] responseData = webClient.UploadData(geturl, gettype == 0 ? "POST" : "GET", postData);
                string srcString = Encoding.UTF8.GetString(responseData);
                srcString = Encoding.UTF8.GetString(responseData);

                context.Response.Write(new
                {
                    code = 200,
                    msg = "^_^ Request Success !",
                    data = srcString
                }.DocumentsToJson());
            }
            catch (Exception ex)
            {
                context.Response.Write(new
                {
                    code = 500,
                    msg = ">_< Request Error!",
                    data = ex.Message
                }.DocumentsToJson());
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}