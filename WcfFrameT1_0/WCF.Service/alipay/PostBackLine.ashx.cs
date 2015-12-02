using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sucool.InternetFace;
using Extensions.LogExtension;
using WCF.Common;
using WCF.Alipay;



namespace WCF.Service.alipay
{
    /// <summary>
    /// Line 付款成功 异步请求处理
    /// create by gd 
    /// 2015.04.28
    /// </summary>    
    public class PostBackLine : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string WriteLogUrl = AlipayConfig.WriteLogUrl;
            string WriteLogPath = AlipayConfig.WriteLogPath;
            LINEEntranceManager zfbmana = new LINEEntranceManager();
            LineResponsePara retmodel = zfbmana.LinePayCallBack(context);


            //交易成功
            if (retmodel.isPaySuccess)
            {
                #region
                string order_no = retmodel.orderNumber;
                try
                {
                    //确认付款
                    int result = 1;
                    //new ShopCart_BLL().OrderPaySuccess(order_no,
                    //    Convert.ToDecimal(retmodel.paymentMoney),
                    //    new EventMarketing_BLL().GetSKSystemBalanceMoney(
                    //GoldBook_Enum.SKSystemSendYEEnum.BuyingSuccess));

                    if (result == 1)
                    {
                        //确认成功                            
                        //业务逻辑添加
                    }
                    else
                    {
                        string.Format("[alipay_line] 订单：{0}交易成功,但是确认失败！请联系技术人员调试！ 结果编号：{1}  DateTime:{2}",
                           order_no,
                           result.ToString(),
                           DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
                           .WriteNoteBook(WriteLogUrl, WriteLogPath);

                        return;
                    }

                    //发送支付成功消息
                    //请不要删除
                    context.Response.Write("{\"code\":\"00\",\"msg\":\"单据信息客户端处处理完成\"}");
                }
                catch (Exception ex)
                {
                    string.Format("[alipay_line] 订单：{0}交易成功,但是确认异常！请联系技术人员调试！ 异常信息：{1}  DateTime:{2}",
                           order_no,
                           ex.Message,
                           DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
                           .WriteNoteBook(WriteLogUrl, WriteLogPath);

                    //发送支付成功消息
                    //请不要删除
                    context.Response.Write("{\"code\":\"02\",\"msg\":\"单据信息客户端处处理失败\"}");
                }
                #endregion
            }
            //交易失败
            else
            {
                string.Format("[alipay_line] 订单：{0}交易失败！ msg:{2}  DateTime:{1}",
                           retmodel.orderNumber,
                           DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                           retmodel.resultMsg)
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