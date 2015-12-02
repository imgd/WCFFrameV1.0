using System;
using System.Collections.Generic;
using System.Text;

    public class APIURLConfig
    {
        static APIURLConfig()
        {
            //一键支付PC端网页收银台前缀
            payWebPrefix = "https://ok.yeepay.com/payweb";//生产环境
            //payWebPrefix = "http://mobiletest.yeepay.com/payweb";//测试环境

            //一键支付移动终端网页收银台前缀
            payMobilePrefix = "https://ok.yeepay.com/paymobile";//生产环境
            //payMobilePrefix = "http://mobiletest.yeepay.com/paymobile";//测试环境
            
            //一键支付API前缀
            apiprefix = "https://ok.yeepay.com/payapi";//生产环境
            //apiprefix = "http://mobiletest.yeepay.com/testpayapi";//测试环境
            

            //商户通用接口前缀
            merchantPrefix = "https://ok.yeepay.com/merchant";//生产环境
            //merchantPrefix = "http://mobiletest.yeepay.com/merchant";//测试环境
            

            //PC端网页收银台支付地址
            pcwebURI = "/api/pay/request";

            //移动终端网页收银台支付地址
            webpayURI = "/api/pay/request";

            
            //绑卡支付接口
            bindpayURI = "/api/bankcard/bind/pay/request";

            //发生短信验证码接口
            sendValidateCodeURI = "/api/validatecode/send";

            //确认支付
            confirmPayURI = "/api/async/bankcard/pay/confirm/validatecode";

            //支付结果查询接口
            queryPayResultURI="/api/query/order";

            //获取绑卡列表
            bindlistURI = "/api/bankcard/bind/list";

            //根据银行卡卡号检查银行卡是否可以使用一键支付
            bankcardCheckURI = "/api/bankcard/check";

            //解绑接口
            unbindURI = "/api/bankcard/unbind";

            //直接退款
            directFundURI = "/query_server/direct_refund";

            //交易记录查询
            queryOrderURI = "/query_server/pay_single";

            //退款订单查询
            queryRefundURI = "/query_server/refund_single";

            //获取消费清算对账单
            clearPayDataURI = "/query_server/pay_clear_data";

            //获取退款清算对账单
            clearRefundDataURI = "/query_server/refund_clear_data";

            //异步支付页面url
            callbackUrl = "http://wcfservice.sucool.com/Alipay/PostBackYB.ashx";

            //同步支付页面url
            fcallbackUrl = "http://www.sucool.com/PayProcess/PayAfter/PayResult_YeePay.aspx";

            //同步支付页面url touch
            fcallbackUrl_Mobile = "http://m.sucool.com/PayProcess/PayAfter/PayResult_YeePay.aspx";

            //商户AESkey
            merchantAesKey = AES.GenerateAESKey();
        }

        public static string fcallbackUrl_Mobile
        { get; set; }
        public static string merchantAesKey
        { get; set; }
        public static string callbackUrl
        { get; set; }
        public static string fcallbackUrl
        { get; set; }

        public static string payWebPrefix
        { get; set; }

        public static string payMobilePrefix
        { get; set; }

        public static string apiprefix
        { get; set; }

        public static string merchantPrefix
        { get; set; }

        public static string pcwebURI
        { get; set; }

        public static string webpayURI
        { get; set; }

        public static string creditWebpayURI
        { get; set; }

        public static string debitWebpayURI
        { get; set; }

        public static string bindpayURI
        { get; set; }

        public static string bindlistURI
        { get; set; }

        public static string bindcheckURI
        { get; set; }

        public static string bankcardCheckURI
        { get; set; }

        public static string queryPayResultURI
        { get; set; }

        public static string unbindURI
        { get; set; }

        public static string directFundURI
        { get; set; }

        public static string queryOrderURI
        { get; set; }

        public static string queryRefundURI
        { get; set; }

        public static string sendValidateCodeURI
        { get; set; }

        public static string confirmPayURI
        { get; set; }

        public static string clearPayDataURI
        { get; set; }

        public static string clearRefundDataURI
        { get; set; }
    }

