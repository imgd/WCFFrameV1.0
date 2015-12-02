using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alipay;
using Com.Alipay.Touch;
using System.Collections.Specialized;
using System.Web;
using Extensions.VerificationExtension;
using Extensions.StringExtension;
using Extensions.ConvertExtension;
using WxPayAPI;
using Sucool.InternetFace.Alipay;
using WCF.Alipay;

namespace Sucool.InternetFace
{
    /// <summary>
    /// 微信支付 配置类
    /// code by gd
    /// 2015/7/3
    /// </summary>
    public class WXEntranceManager
    {
        /// <summary>
        /// wx支付提交请求
        /// </summary>
        /// <param name="clientOpenId"></param>
        /// <param name="totalfell">注意这里单位是 分</param>
        /// <returns></returns>
        public string WXJSApi_SubRequest(string clientOpenId, string orderNumber, string totalfell)
        {
            if (!orderNumber.OrderNumberVerify())
            {
                return "-1";//提交失败 参数异常
            }


            JsApiPay jsApiPay = new JsApiPay();
            jsApiPay.openid = clientOpenId;
            jsApiPay.total_fee = AlipayConfig.MoneyFormatEco(totalfell).ParseInt();
            jsApiPay.orderNumber = orderNumber;
            try
            {
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                string wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数                    
                Log.Debug(this.GetType().ToString(), "wxJsApiParam : " + wxJsApiParam);

                return wxJsApiParam;
            }
            catch (Exception ex)
            {
                return "-2";//提交异常
            }
        }
        /// <summary>
        /// wx支付提交请求
        /// </summary>
        /// <param name="clientOpenId"></param>
        /// <param name="totalfell">注意这里单位是 分</param>
        /// <returns></returns>
        public string WXNAVITE_SubRequest(string orderNumber, string totalfell)
        {
            if (!orderNumber.OrderNumberVerify())
            {
                return "-1";//提交失败 参数异常
            }


            JsApiPay jsApiPay = new JsApiPay();
            jsApiPay.total_fee = AlipayConfig.MoneyFormatEco(totalfell).ParseInt();
            jsApiPay.orderNumber = orderNumber;           
            try
            {
                string url;
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult(true, out url);
                string wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数                    
                Log.Debug(this.GetType().ToString(), "wxJsApiParam : " + wxJsApiParam);
                return url;
            }
            catch (Exception ex)
            {
                return "-2";//提交异常
            }
        }

        /// <summary>
        /// wx异步返回处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public WxPayAPI.ResultNotify.WXPayBack WXJSApi_Notify(HttpContext context)
        {
            ResultNotify resultNotify = new ResultNotify(context);
            return resultNotify.ProcessNotify();
        }
        /// <summary>
        /// wx异步返回处理 APP异步处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public WxPayAPI.ResultNotify.WXPayBack WXJSApi_Notify(HttpContext context,bool isApp)
        {
            ResultNotify resultNotify = new ResultNotify(context);
            return resultNotify.ProcessNotify(true);
        }
    }

}
