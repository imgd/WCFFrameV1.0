using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sucool.InternetFace;
using Extensions.LogExtension;
using WCF.Common;
using Sucool.InternetFace.Alipay;
using WCF.Alipay;



namespace WCF.Service.alipay
{
    /// <summary>
    /// ZFB付款成功 异步请求处理
    /// create by gd 
    /// 2015.04.15
    /// </summary>    
    public class PostBackZFB_mobile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string WriteLogUrl = AlipayConfig.WriteLogUrl;
            string WriteLogPath = AlipayConfig.WriteLogPath;
            ZFBEntranceManager zfbmana = new ZFBEntranceManager();
            ZFBReturnPara_Model retmodel = zfbmana.ZFBCallBack(context, true, true);

            //返回成功
            if (retmodel.iscallbacksuccess)
            {
                //交易成功
                if (retmodel.ispaysuccess)
                {
                    #region
                    string order_no = retmodel.order_no;
                    try
                    {
                        //确认付款
                        int result = 1;// new ShopCart_BLL().OrderPaySuccess(order_no,
                        //    Convert.ToDecimal(retmodel.total_fee),
                        //    new EventMarketing_BLL().GetSKSystemBalanceMoney(
                        //GoldBook_Enum.SKSystemSendYEEnum.BuyingSuccess));

                        if (result == 1)
                        {
                            //确认成功                            
                            //业务逻辑添加
                        }
                        else
                        {
                            string.Format("[alipay_zfb_mobile] 订单：{0}交易成功,但是确认失败！请联系技术人员调试！ 结果编号：{1}  DateTime:{2}",
                               order_no,
                               result.ToString(),
                               DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
                               .WriteNoteBook(WriteLogUrl, WriteLogPath);

                        }

                    }
                    catch (Exception ex)
                    {
                        string.Format("[alipay_zfb_mobile] 订单：{0}交易成功,但是确认异常！请联系技术人员调试！ 异常信息：{1}  DateTime:{2}",
                               order_no,
                               ex.Message,
                               DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
                               .WriteNoteBook(WriteLogUrl, WriteLogPath);
                    }
                    #endregion
                }
                //交易失败
                else if (!retmodel.ispaysuccess && !retmodel.iscallbacksuccess)
                {
                    string.Format("[alipay_zfb_mobile] 订单：{0}交易或者退款申请失败！  DateTime:{1}",
                               retmodel.order_no,
                               DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
                               .WriteNoteBook(WriteLogUrl, WriteLogPath);
                }
            }
            else
            {
                string.Format("[alipay_zfb_mobile] 订单：{0}交易失败！  DateTime:{1}",
                               retmodel.order_no,
                               DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
                               .WriteNoteBook(WriteLogUrl, WriteLogPath);
            }

            //发送支付成功消息
            //请不要删除
            context.Response.Write(retmodel.sendzfbcallbackmessage);
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