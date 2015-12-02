using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sucool.InternetFace;
using Extensions.LogExtension;
using Sucool.InternetFace.Alipay;
using WCF.Alipay;



namespace WCF.Service.alipay
{
    /// <summary>
    /// ZFB付款成功 异步请求处理
    /// create by gd 
    /// 2015.04.15
    /// </summary>    
    public class PostBackWX_APP : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string WriteLogUrl = AlipayConfig.WriteLogUrl;
            string WriteLogPath = AlipayConfig.WriteLogPath;
            WXEntranceManager zfbmana = new WXEntranceManager();
            WxPayAPI.ResultNotify.WXPayBack results = zfbmana.WXJSApi_Notify(context,true);


            //交易成功
            if (results.ResultCode == 1)
            {
                //交易成功代码
                #region
                //string order_no = results.OrderNumber;
                //try
                //{
                //                     
                //    //确认付款
                //    int result =1; 

                //    if (result > 1)
                //    {
                //        //确认成功                            
                //        //业务逻辑添加
                //    }
                //    else
                //    {
                //        string.Format("[alipay_WX_touch] 订单：{0}交易成功,但是确认失败！请联系技术人员调试！ 结果编号：{1}  DateTime:{2}",
                //           order_no,
                //           result.ToString(),
                //           DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
                //           .WriteNoteBook(WriteLogUrl, WriteLogPath);

                //    }

                //}
                //catch (Exception ex)
                //{
                //    string.Format("[alipay_WX_touch] 订单：{0}交易成功,但是确认异常！请联系技术人员调试！ 异常信息：{1}  DateTime:{2}",
                //           order_no,
                //           ex.Message,
                //           DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
                //           .WriteNoteBook(WriteLogUrl, WriteLogPath);
                //}
                #endregion
            }
            //交易失败
            else
            {
                string.Format("[alipay_WX_touch] 订单：{0}交易或者退款申请失败！  DateTime:{1}",
                           results.OrderNumber,
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