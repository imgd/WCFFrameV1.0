using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sucool.InternetFace;
using Extensions.StringExtension;
using Extensions.LogExtension;
using WCF.Common;
using WCF.Alipay;

namespace WCF.Service.alipay
{
    /// <summary>
    /// UPOP银联付款成功 异步请求处理
    /// create by gd 
    /// 2015.04.15
    /// </summary>    
    public class PostBackUPOP : IHttpHandler
    {        
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string WriteLogUrl = AlipayConfig.WriteLogUrl;
            string WriteLogPath = AlipayConfig.WriteLogPath;
            UPOPEntranceManager upopmana = new UPOPEntranceManager();
            UPOPReturlPara retmodel = upopmana.UPOPCallBack(context);

            //交易成功
            if (retmodel.isSuccessPay)
            {
                #region
                string order_no = retmodel.orderNumber;
                try
                {
                    //确认付款
                    int result = 1;                        

                    if (result == 1)
                    {
                        //确认成功                            
                        //业务逻辑添加
                    }
                    else
                    {
                        string.Format("[alipay_zfb] 订单：{0}交易成功,但是确认失败！请联系技术人员调试！ 结果编号：{1}  DateTime:{2}",
                           order_no,
                           result.ToString(),
                           DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
                           .WriteNoteBook(WriteLogUrl, WriteLogPath);

                        return;
                    }
                }
                catch (Exception ex)
                {
                    string.Format("[alipay_upop] 订单：{0}交易成功,但是确认异常！请联系技术人员调试！ 异常信息：{1}  DateTime:{2}",
                           order_no,
                           ex.Message,
                           DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
                           .WriteNoteBook(WriteLogUrl, WriteLogPath);
                }
                #endregion
            }
            //交易失败
            else
            {
                string.Format("[alipay_upop] 订单：{0}交易或者退款申请失败！  DateTime:{1}",
                           retmodel.orderNumber,
                           DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
                           .WriteNoteBook(WriteLogUrl, WriteLogPath);
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