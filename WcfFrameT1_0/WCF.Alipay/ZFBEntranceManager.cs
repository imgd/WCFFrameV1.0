using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alipay;
using Com.Alipay.Touch;
using System.Collections.Specialized;
using System.Web;
using Extensions.WebRequestExtension;
using Extensions.LogExtension;
using System.Xml;

namespace WCF.Alipay
{
    /// <summary>
    /// 支付宝 配置类
    /// code by gd
    /// 2013/7/15
    /// </summary>
    public class ZFBEntranceManager
    {
        #region pc web
        /// <summary>
        /// 支付宝入口请求
        /// </summary>
        /// <param name="zfb"></param>
        /// <returns></returns>
        public string ZFBSendRequestWriter(ZFBPara_Model zfb)
        {

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            #region

            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("out_trade_no", zfb.out_trade_no);
            sParaTemp.Add("subject", zfb.subject);
            sParaTemp.Add("body", zfb.body);
            sParaTemp.Add("total_fee", zfb.total_fee);
            //默认支付方式，代码见“即时到帐接口”技术文档
            sParaTemp.Add("paymethod", "");
            //默认网银代号，代号列表见“即时到帐接口”技术文档“附录”→“银行列表”            
            sParaTemp.Add("defaultbank", "");
            //防钓鱼时间戳            
            sParaTemp.Add("anti_phishing_key", "");
            //获取客户端的IP地址，建议：编写获取客户端IP地址的程序            
            sParaTemp.Add("exter_invoke_ip", "");
            //自定义参数，可存放任何内容（除=、&等特殊字符外），不会显示在页面上            
            sParaTemp.Add("extra_common_param", "");
            //默认买家支付宝账号            
            sParaTemp.Add("buyer_email", "");
            //提成类型，该值为固定值：10，不需要修改            
            sParaTemp.Add("royalty_type", "");
            //提成信息集            
            sParaTemp.Add("royalty_parameters", "");
            #endregion
            //构造即时到帐接口表单提交HTML数据，无需修改
            Service ali = new Service();
            string sHtmlText = ali.Create_direct_pay_by_user(sParaTemp);
            return sHtmlText;
        }
        /// <summary>
        /// 支付宝异步返回订单交易信息 post
        /// </summary>
        /// <param name="context">当前上下文</param>
        /// <returns></returns>
        public ZFBReturnPara_Model ZFBCallBack(HttpContext context)
        {
            ZFBReturnPara_Model zfbpara = new ZFBReturnPara_Model();
            zfbpara.iscallbacksuccess = false;
            SortedDictionary<string, string> sPara = GetRequestPost(context);
            if (sPara.Count > 0)
            {
                Com.Alipay.Notify aliNotify = new Com.Alipay.Notify();
                bool verifyResult = aliNotify.Verify(sPara, context.Request.Form["notify_id"], context.Request.Form["sign"]);
                if (verifyResult)//验证成功
                {
                    #region
                    //退款状态，付款成功 没有此参数
                    string refund_status = "refund_status".GetFormString();
                    //交易状态
                    string trade_status = "trade_status".GetFormString();

                    zfbpara.trade_no = context.Request.Form["trade_no"];
                    zfbpara.order_no = context.Request.Form["out_trade_no"];
                    zfbpara.total_fee = context.Request.Form["total_fee"];
                    zfbpara.ispaysuccess = ((trade_status == "TRADE_SUCCESS" || trade_status == "TRADE_FINISHED") && refund_status == "") ? true : false;
                    zfbpara.isallreturn = ((trade_status == "TRADE_SUCCESS" || trade_status == "TRADE_FINISHED") && refund_status == "REFUND_SUCCESS") ? true : false;
                    #endregion
                    zfbpara.iscallbacksuccess = true;
                    zfbpara.sendzfbcallbackmessage = "success";
                }
                else
                    zfbpara.sendzfbcallbackmessage = "fail";
            }
            else
                zfbpara.sendzfbcallbackmessage = "无通知参数";

            return zfbpara;
        }
        /// <summary>
        /// 支付宝 返回 前端订单交易信息 get
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public ZFBReturnPara_Model ZFBReturn(HttpContext context)
        {
            ZFBReturnPara_Model zfbpara = new ZFBReturnPara_Model();
            SortedDictionary<string, string> sPara = GetRequestGet(context);
            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Com.Alipay.Notify aliNotify = new Com.Alipay.Notify();
                bool verifyResult = aliNotify.Verify(sPara, context.Request.QueryString["notify_id"], context.Request.QueryString["sign"]);
                if (verifyResult)//验证成功
                {
                    #region
                    //交易状态
                    string trade_status = "trade_status".GetUrlString();

                    zfbpara.trade_no = "trade_no".GetUrlString();
                    zfbpara.order_no = "out_trade_no".GetUrlString();
                    zfbpara.total_fee = "total_fee".GetUrlString();

                    zfbpara.ispaysuccess = (trade_status == "TRADE_SUCCESS" || trade_status == "TRADE_FINISHED") ? true : false;
                    zfbpara.isallreturn = false;
                    #endregion
                    zfbpara.iscallbacksuccess = true;
                    zfbpara.sendzfbcallbackmessage = "success";
                }
                else
                    zfbpara.sendzfbcallbackmessage = "fail";
            }
            else
                zfbpara.sendzfbcallbackmessage = "无通知参数";

            return zfbpara;

        }

        #endregion

        #region touch
        /// <summary>
        /// 支付宝入口请求
        /// </summary>
        /// <param name="zfb"></param>
        /// <returns></returns>
        public string ZFBSendRequestWriter(ZFBPara_Model zfb, bool isTouch)
        {

            //返回格式
            string format = "xml";
            //必填，不需要修改

            //返回格式
            string v = "2.0";
            //必填，不需要修改

            //请求号
            string req_id = DateTime.Now.ToString("yyyyMMddHHmmss");
            //必填，须保证每次请求都是唯一

            //req_data详细信息


            //商户订单号
            string out_trade_no = zfb.out_trade_no;
            //商户网站订单系统中唯一订单号，必填

            //订单名称
            string subject = zfb.subject;
            //必填

            //付款金额
            string total_fee = zfb.total_fee;
            //必填

            //请求业务参数详细
            string req_dataToken = "<direct_trade_create_req><notify_url>" + Com.Alipay.Touch.Config.notify_url + "</notify_url><call_back_url>" + Com.Alipay.Touch.Config.call_back_url + "</call_back_url><seller_account_name>" + Com.Alipay.Touch.Config.Seller_email + "</seller_account_name><out_trade_no>" + out_trade_no + "</out_trade_no><subject>" + subject + "</subject><total_fee>" + total_fee + "</total_fee><merchant_url>" + Com.Alipay.Touch.Config.merchant_url + "</merchant_url></direct_trade_create_req>";
            //必填

            //把请求参数打包成数组
            Dictionary<string, string> sParaTempToken = new Dictionary<string, string>();
            sParaTempToken.Add("partner", Com.Alipay.Touch.Config.Partner);
            sParaTempToken.Add("_input_charset", Com.Alipay.Touch.Config.Input_charset.ToLower());
            sParaTempToken.Add("sec_id", Com.Alipay.Touch.Config.Sign_type.ToUpper());
            sParaTempToken.Add("service", "alipay.wap.trade.create.direct");
            sParaTempToken.Add("format", format);
            sParaTempToken.Add("v", v);
            sParaTempToken.Add("req_id", req_id);
            sParaTempToken.Add("req_data", req_dataToken);

            //建立请求
            string sHtmlTextToken = Com.Alipay.Touch.Submit.BuildRequest(Com.Alipay.Touch.Config.gateway_new, sParaTempToken);
            //URLDECODE返回的信息
            Encoding code = Encoding.GetEncoding(Com.Alipay.Touch.Config.Input_charset);
            sHtmlTextToken = HttpUtility.UrlDecode(sHtmlTextToken, code);

            //解析远程模拟提交后返回的信息
            Dictionary<string, string> dicHtmlTextToken = Com.Alipay.Touch.Submit.ParseResponse(sHtmlTextToken);

            //获取token
            string request_token = dicHtmlTextToken["request_token"];

            ////////////////////////////////////////////根据授权码token调用交易接口alipay.wap.auth.authAndExecute////////////////////////////////////////////


            //业务详细
            string req_data = "<auth_and_execute_req><request_token>" + request_token + "</request_token></auth_and_execute_req>";
            //必填

            //把请求参数打包成数组
            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("partner", Com.Alipay.Touch.Config.Partner);
            sParaTemp.Add("_input_charset", Com.Alipay.Touch.Config.Input_charset.ToLower());
            sParaTemp.Add("sec_id", Com.Alipay.Touch.Config.Sign_type.ToUpper());
            sParaTemp.Add("service", "alipay.wap.auth.authAndExecute");
            sParaTemp.Add("format", format);
            sParaTemp.Add("v", v);
            sParaTemp.Add("req_data", req_data);

            //建立请求
            string sHtmlText = Com.Alipay.Touch.Submit.BuildRequest(Com.Alipay.Touch.Config.gateway_new, sParaTemp, "get", "确认");
            return sHtmlText;
        }
        /// <summary>
        /// 支付宝 返回 前端订单交易信息 get
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public ZFBReturnPara_Model ZFBCallBack(HttpContext context, bool isTouch)
        {
            ZFBReturnPara_Model zfbpara = new ZFBReturnPara_Model();
            Dictionary<string, string> sPara = GetRequestInputStream(context);
            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Com.Alipay.Touch.Notify aliNotify = new Com.Alipay.Touch.Notify();
                bool verifyResult = aliNotify.VerifyNotify(sPara, sPara["sign"]);

                if (verifyResult)//验证成功
                {
                    //XML解析notify_data数据
                    try
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(sPara["notify_data"]);
                        //商户订单号
                        string out_trade_no = xmlDoc.SelectSingleNode("/notify/out_trade_no").InnerText;
                        //支付宝交易号
                        string trade_no = xmlDoc.SelectSingleNode("/notify/trade_no").InnerText;
                        //交易状态
                        string trade_status = xmlDoc.SelectSingleNode("/notify/trade_status").InnerText;
                        //交易状态
                        string total_fee = xmlDoc.SelectSingleNode("/notify/total_fee").InnerText;

                        if (trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS")
                        {
                            zfbpara.trade_no = trade_no;
                            zfbpara.order_no = out_trade_no;
                            zfbpara.total_fee = total_fee;

                            zfbpara.ispaysuccess = (trade_status == "TRADE_SUCCESS" || trade_status == "TRADE_FINISHED") ? true : false;
                            zfbpara.isallreturn = false;
                            zfbpara.iscallbacksuccess = true;
                            zfbpara.sendzfbcallbackmessage = "success";
                        }
                        else
                        {
                            zfbpara.sendzfbcallbackmessage = "fail";
                        }

                    }
                    catch (Exception exc)
                    {
                        zfbpara.sendzfbcallbackmessage = exc.Message;
                    }

                }
                else//验证失败
                {
                    zfbpara.sendzfbcallbackmessage = "fail";
                }
            }
            else
            {
                zfbpara.sendzfbcallbackmessage = "无通知参数";
            }

            return zfbpara;

        }

        #endregion

        #region mobile

        /// <summary>
        /// 支付宝移动端返回签名认证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public ZFBReturnPara_Model ZFBCallBack(HttpContext context,bool isTouch,bool isMobile)
        {
            ZFBReturnPara_Model zfbpara = new ZFBReturnPara_Model();
            zfbpara.iscallbacksuccess = false;
            SortedDictionary<string, string> sPara = GetRequestPost(context);
            if (sPara.Count > 0)
            {
                bool verifyResult = Com.Alipay.Touch.RSAFromPkcs8.verify(
                    Com.Alipay.Touch.RSAFromPkcs8.GetResponseMysign(sPara, 
                    Com.Alipay.Touch.Config.rsa_publickey,"RSA","UTF-8")
                    ,sPara["sign"],
                    Com.Alipay.Touch.Config.rsa_publickey,"UTF-8"); 

                if (verifyResult)//验证成功
                {
                    #region
                    //退款状态，付款成功 没有此参数
                    string refund_status = sPara["refund_status"];
                    //交易状态
                    string trade_status = sPara["trade_status"];

                    zfbpara.trade_no = sPara["trade_no"];
                    zfbpara.order_no = sPara["out_trade_no"];
                    zfbpara.total_fee = sPara["total_fee"];

                    zfbpara.ispaysuccess = ((trade_status == "TRADE_SUCCESS" || trade_status == "TRADE_FINISHED") && refund_status == "") ? true : false;
                    zfbpara.isallreturn = ((trade_status == "TRADE_SUCCESS" || trade_status == "TRADE_FINISHED") && refund_status == "REFUND_SUCCESS") ? true : false;
                    #endregion
                    zfbpara.iscallbacksuccess = true;
                    zfbpara.sendzfbcallbackmessage = "success";
                }
                else
                    zfbpara.sendzfbcallbackmessage = "fail";
            }
            else
                zfbpara.sendzfbcallbackmessage = "无通知参数";

            return zfbpara;
        }
        #endregion

        #region private

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        private SortedDictionary<string, string> GetRequestPost(HttpContext context)
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            coll = context.Request.Form;
            String[] requestItem = coll.AllKeys;
            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], context.Request.Form[requestItem[i]]);
            }
            return sArray;
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet(HttpContext context)
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            coll = context.Request.QueryString;
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], context.Request.QueryString[requestItem[i]]);
            }
            return sArray;
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public Dictionary<string, string> GetRequestPost(HttpContext context, bool isDictionary)
        {
            int i = 0;
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = context.Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], context.Request.Form[requestItem[i]]);
            }

            return sArray;
        }


        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public Dictionary<string, string> GetRequestInputStream(HttpContext context)
        {
            byte[] byts = new byte[context.Request.InputStream.Length];
            HttpContext.Current.Request.InputStream.Read(byts, 0, byts.Length);
            string req = System.Text.Encoding.UTF8.GetString(byts);
            req = HttpContext.Current.Server.UrlDecode(req);
            List<string> lx = req.Split('&').ToList<string>();
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            foreach (string str in lx)
            {
                string[] arr = str.Split('=');
                if (arr.Length == 2)
                {
                    sArray.Add(arr[0], arr[1]);
                }
            }
            return sArray;
        }

        #endregion

    }

    /// <summary>
    /// 支付宝 入口配置实体
    /// </summary>
    public class ZFBPara_Model
    {
        public string showUrl { get; set; }
        /// <summary>
        /// 商家订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 商品标题
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// 商品详情
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 商品交易金额
        /// </summary>
        public string total_fee { get; set; }
    }

    /// <summary>
    /// 支付宝 callback 参数
    /// </summary>
    public class ZFBReturnPara_Model
    {
        //订单参数

        /// <summary>
        /// 支付宝交易号
        /// </summary>
        public string trade_no { get; set; }
        /// <summary>
        /// 商家订单号
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 支付宝交易金额
        /// </summary>
        public string total_fee { get; set; }

        /// <summary>
        /// 是否是交易成功
        /// </summary>
        public bool ispaysuccess { get; set; }
        /// <summary>
        /// 是否是全额退款成功
        /// </summary>
        public bool isallreturn { get; set; }

        /// <summary>
        /// 是否返回成功请求
        /// </summary>
        public bool iscallbacksuccess { get; set; }
        /// <summary>
        /// 发送给支付宝 交易请求状态
        /// </summary>
        public string sendzfbcallbackmessage { get; set; }

    }
}
