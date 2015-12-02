using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Sucool.InternetFace.Alipay.YB.Util;
using Extensions.JsonExtension;
using Extensions.ConvertExtension;
using WCF.Common;
using Extensions.LogExtension;
using Extensions.StringExtension;

namespace WCF.Alipay
{
    /// <summary>
    /// 易宝 入口支付  参数配置类
    /// code by gd
    /// 2015/5/4
    /// </summary>
    public class YBEntranceManager
    {
        /// <summary>
        /// 易宝支付
        /// </summary>
        /// <returns></returns>
        public string YBPaySendRequesWriter(YBRequestPara para, bool isMobile)
        {
            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("merchantaccount", Config.merchantAccount);
            sd.Add("amount", para.amount);
            sd.Add("currency", 156);
            sd.Add("identityid", "user_" + para.userId.DESDecrypt());
            sd.Add("identitytype", 2);
            sd.Add("orderid", para.orderid);
            sd.Add("orderexpdate", 60);
            sd.Add("productcatalog", "48");
            sd.Add("productdesc", "速库美味商品");
            sd.Add("productname", "速库美味商品");
            //DateTime t1 = DateTime.Now;
            //DateTime t2 = new DateTime(1970, 1, 1);
            //double t = t1.Subtract(t2).TotalSeconds;
            //int transtime = (int)t;

            sd.Add("transtime", para.orderTime.DateTimeConvertTimeStamp().ParseInt());
            sd.Add("userip", "255.255.255.255");
            sd.Add("terminaltype", 3);
            sd.Add("terminalid", "nothing");
            sd.Add("callbackurl", APIURLConfig.callbackUrl);
            sd.Add("fcallbackurl", isMobile ? APIURLConfig.fcallbackUrl_Mobile : APIURLConfig.fcallbackUrl);
            sd.Add("userua", "");
            sd.Add("paytypes", "1|2");
            //生成RSA签名
            string sign = EncryptUtil.handleRSA(sd, Config.merchantPrivatekey);
            sd.Add("sign", sign);
            //将网页支付对象转换为json字符串
            string merchantAesKey = APIURLConfig.merchantAesKey;

            string wpinfo_json = Newtonsoft.Json.JsonConvert.SerializeObject(sd);
            string datastring = AES.Encrypt(wpinfo_json, merchantAesKey);
            string encryptkey = RSAFromPkcs8.encryptData(merchantAesKey, Config.yibaoPublickey, "UTF-8");

            string postParams = "data=" + HttpUtility.UrlEncode(datastring)
                + "&encryptkey=" + HttpUtility.UrlEncode(encryptkey)
                + "&merchantaccount=" + Config.merchantAccount;
            string url = (!isMobile ? APIURLConfig.payWebPrefix : APIURLConfig.payMobilePrefix)
                + (!isMobile ? APIURLConfig.pcwebURI : APIURLConfig.webpayURI)
                + "?" + postParams;

            return url;
            //return YBPAY.CreatePayByPostHTML(HttpUtility.UrlEncode(datastring),
            //    HttpUtility.UrlEncode(encryptkey),
            //    Config.merchantAccount,isMobile);
        }

        /// <summary>
        /// 易宝异步返回
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public YBResponsePara YBCallBack(HttpContext context)
        {
            YBResponsePara result = new YBResponsePara()
            {
                isPaySuccess = false,
                orderNumber = "",
                paymentMoney = 0,
                resultMsg = ""
            };

            try
            {
                
                if (context.Request["data"].IsNull() ||
                    context.Request["encryptkey"].IsNull())
                {
                    result.resultMsg = "回调参数不正确";
                    return result;
                }
                //回调中的参数data
                string data = context.Request["data"].ToString();
                //回调中的参数encryptkey  
                string encryptkey = context.Request["encryptkey"].ToString();
                //解密易宝支付回调结果
                string callback_result = YJPayUtil.checkYbCallbackResult(data, encryptkey);
                if (callback_result != "验签未通过")
                {
                    //string descstring = AES.Decrypt(data, APIURLConfig.merchantAesKey);
                    Dictionary<string, object> responsePara = callback_result.JsonToDocument<Dictionary<string, object>>();

                    if (!responsePara.IsNull())
                    {


                        result.isPaySuccess = true;
                        result.paymentMoney = AlipayConfig.MoneyFormatDco(responsePara["amount"].ToString());
                        result.orderNumber = responsePara["orderid"].ToString();
                        result.resultMsg = "返回成功";
                    }
                }
            }
            catch (Exception err)
            {
                result.resultMsg = err.Message;
            }

            return result;
        }

    }

    /// <summary>
    /// 易宝支付返回信息
    /// </summary>
    public class YBResponsePara
    {
        /// <summary>
        /// 是否支付成功
        /// </summary>
        public bool isPaySuccess { get; set; }
        /// <summary>
        /// 支付订单号
        /// </summary>
        public string orderNumber { get; set; }
        /// <summary>
        /// 支付的金额
        /// </summary>
        public decimal paymentMoney { get; set; }
        /// <summary>
        /// 返回的支付信息描述
        /// </summary>
        public string resultMsg { get; set; }
    }

    public class YBRequestPara
    {
        /// <summary>
        /// 支付金额为 单位分
        /// </summary>
        public long amount { get; set; }

        /// <summary>
        /// 商家订单号
        /// </summary>
        public string orderid { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime orderTime { get; set; }

    }
}
