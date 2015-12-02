using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alipay;
using System.Web;
using Extensions.ConvertExtension;
using Extensions.JsonExtension;
using System.IO;
using Sucool.InternetFace.Alipay;

namespace WCF.Alipay
{
    /// <summary>
    /// 中联信通 入口支付  参数配置类
    /// code by gd
    /// 2015/4/28
    /// </summary>
    public class LINEEntranceManager
    {
        protected Object lockobject = new Object();

        /// <summary>
        /// 订单支付表单HTML字符串生成
        /// </summary>
        /// <param name="context">请求上下文对象</param>
        /// <param name="para">订单相关参数</param>
        /// <returns></returns>
        public string LinePaySendRequestWriter(LineRequestPara para)
        {
            string key = string.Empty;
            string keyValue = string.Empty;
            string signValue = string.Empty;
            string sign = string.Empty;
            RSAOperate Rdaop = new RSAOperate();

            Dictionary<string, string> dic = new Dictionary<string, string>() { 
             {"outOrderId",para.outOrderId},
             {"totalAmount",para.totalAmount.ToString()},
             {"goodsName",para.goodsName},
             {"goodsExplain",para.goodsExplain},
             {"merUrl",para.merUrl},
             {"noticeUrl",para.noticeUrl},
             {"bankCardType",para.bankCardType},
             {"bankCode",para.bankCode},
             {"orderCreateTime",para.orderCreateTime},
             {"lastPayTime",para.lastPayTime}
            };


            signValue = Rdaop.GetUrlParamString(CreateRquestUrlPara(para), RSASign.GetPayRSAParamSort());
            //提交参数加密
            sign = RSASign.GetMD5RSA(signValue + ProperConst.Key);

            var paraSort = RSASign.GetPayParamSort().ToList();


            StringBuilder writer = new StringBuilder();
            writer.Append(" <!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> ");
            writer.Append(" <html xmlns=\"http://www.w3.org/1999/xhtml\" > ");
            writer.Append(" <head runat=\"server\"><title>中联信通支付</title> ");
            writer.Append(" <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"/> ");
            writer.Append(" </head><body> ");
            writer.AppendFormat(" <form id=\"formpay\" method=\"post\" action=\"{0}\"> ", ProperConst.payUrl);

            paraSort.ForEach(k =>
            {
                key = k;
                if (key == "merchantCode")
                    keyValue = ProperConst.merchantCode;
                else
                    keyValue = dic[key];

                writer.AppendFormat(" <input type=\"text\" name=\"{0}\" style=\"display:none;\" value=\"{1}\"/> ",
                    key, keyValue);
            });

            writer.AppendFormat(" <input type=\"text\" name=\"sign\"  style=\"display:none;\" value=\"{0}\"/> ", sign);
            writer.Append(" </form> ");
            writer.Append(" <script type=\"text/javascript\"> document.getElementById(\"formpay\").submit();</script> ");

            return writer.ToString();
        }

        /// <summary>
        /// 订单支付成功商家异步
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public LineResponsePara LinePayCallBack(HttpContext context)
        {
            var responseForm = context.Request.Form;
            string orderNumber = responseForm["outOrderId"].IsNull()
                ? string.Empty : responseForm["outOrderId"];

            var response = new LineResponsePara()
            {
                isPaySuccess = false,
                orderNumber = orderNumber,
                paymentMoney = 0,
                resultMsg = string.Empty
            };

            try
            {
                RSAOperate Rdaop = new RSAOperate();
                //处理传输过来的流
                Stream responseStream = context.Request.InputStream;
                StreamReader readStream = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                string RequestStream = readStream.ReadToEnd();
                readStream.Close();


                if (!string.IsNullOrEmpty(RequestStream))
                {
                    string RSAChar = Rdaop.GetUrlParamString(RequestStream, RSASign.GetNoticeRSAParamSort()) + ProperConst.Key;
                    if (Rdaop.GetIsSafty(Rdaop.GetjosnValue(RequestStream, "sign"), RSAChar))//判断是否报文加密后能够匹配
                    {
                        lock (lockobject)//此处建议使用lock锁机制，进行并发控制，防止重复数据混乱
                        {
                            response.isPaySuccess = true;
                            response.orderNumber = responseForm["orderNumber"].IsNull()
                                                   ? string.Empty : responseForm["orderNumber"];
                            response.paymentMoney = responseForm["totalAmount"].IsNull() ? (decimal)0.00
                                                  : AlipayConfig.MoneyFormatDco(responseForm["totalAmount"]); ;
                            response.resultMsg = "支付成功";
                        }
                    }
                    else
                    {
                        response.resultMsg = "返回报文加密信息存在异常";
                        context.Response.Write("{\"code\":\"01\",\"msg\":\"返回报文加密信息存在异常\"}");
                    }
                }
                else
                {
                    response.resultMsg = "返回报文为空存在异常";
                    context.Response.Write("{\"code\":\"02\",\"msg\":\"返回报文为空存在异常\"}");
                }
            }
            catch (Exception ex)
            {
                response.resultMsg = "报文异常" + ex.Message;
                context.Response.Write("{\"code\":\"03\",\"msg\":\"报文异常\"}");
            }

            return response;
        }


        /// <summary>
        /// 生成中联信通 from 提交JSON参数
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        private string CreateRquestUrlPara(LineRequestPara para)
        {
            return para.DocumentsToJson();
        }
    }


    /// <summary>
    /// 支付请求参数
    /// </summary>
    public class LineRequestPara
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string outOrderId { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public long totalAmount { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string goodsName { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string goodsExplain { get; set; }
        /// <summary>
        /// 商户同步地址 get
        /// </summary>
        public string merUrl { get; set; }
        /// <summary>
        /// 商户异步订单处理地址 post
        /// </summary>
        public string noticeUrl { get; set; }
        /// <summary>
        /// 支付银行类型 00 个人 03企业
        /// </summary>
        private string _bankCardType;
        public string bankCardType
        {
            get { return _bankCardType; }
            set
            {
                if (!value.IsInArray("00", "03"))
                {
                    _bankCardType = "00";
                }
                else
                {
                    _bankCardType = value;
                }
            }
        }
        /// <summary>
        /// 银行编码
        /// </summary>
        public string bankCode { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public string orderCreateTime { get; set; }
        /// <summary>
        /// 上次支付时间
        /// </summary>
        public string lastPayTime { get; set; }
    }

    /// <summary>
    /// 支付成功响应
    /// </summary>
    public class LineResponsePara
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
}
