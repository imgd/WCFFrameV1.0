using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using com.unionpay.upop.sdk;
using System.Web.UI;
using WCF.Common;
using Sucool.InternetFace.Alipay;

namespace WCF.Alipay
{
    /// <summary>
    /// 银联 入口支付  参数配置类
    /// code by gd
    /// 2013/7/11
    /// </summary>
    public class UPOPEntranceManager
    {
        //配置文件路径
        private readonly string configPath = System.Web.HttpContext.Current.Server.MapPath("/Config/Unionpay.config");

        /// <summary>
        /// 银联支付发送请求
        /// </summary>
        /// <param name="upa"></param>
        /// <param name="writeEcoding"></param>
        /// <returns></returns>
        public string UPOPSendRequesWriter(UPOPPara upa, out Encoding writeEcoding)
        {
            // 要使用各种Srv必须先使用LoadConf载入配置
            UPOPSrv.LoadConf(configPath);
            // 使用Dictionary保存参数
            System.Collections.Generic.Dictionary<string, string> param = new System.Collections.Generic.Dictionary<string, string>();

            // 填写参数
            param["transType"] = UPOPSrv.TransType.CONSUME;                         // 交易类型，前台只支持CONSUME 和 PRE_AUTH
            param["commodityUrl"] = upa.showProUrl;                                 // 商品URL
            param["commodityName"] = upa.proShowName;                               // 商品名称
            param["commodityUnitPrice"] =AlipayConfig.MoneyFormatEco(upa.price);                                // 商品单价，分为单位
            param["commodityQuantity"] = upa.count;                                 // 商品数量
            param["orderNumber"] = upa.orderNumber;                                 // 订单号，必须唯一
            param["orderAmount"] = AlipayConfig.MoneyFormatEco(upa.total);                                       // 交易金额
            param["orderCurrency"] = UPOPSrv.CURRENCY_CNY;                          // 币种
            param["orderTime"] = DateTime.Now.ToString("yyyyMMddHHmmss");      // 交易时间
            param["customerIp"] = "";                              // 用户IP
            param["frontEndUrl"] = upa.returnUrl;                                   // 前台回调URL
            param["backEndUrl"] = upa.notifyUrl;                                    // 后台回调URL

            // 创建前台交易服务对象
            FrontPaySrv srv = new FrontPaySrv(param);
            // 将前台交易服务对象产生的Html文档写入页面，从而引导用户浏览器重定向
            writeEcoding = srv.Charset; // 指定输出编码

            return srv.CreateHtml();
        }
        /// <summary>
        /// 银联支付成功返回信息 post
        /// </summary>
        /// <param name="http">返回当前上下文状态</param>
        /// <returns></returns>
        public UPOPReturlPara UPOPCallBack(HttpContext http)
        {
            // 要使用各种Srv必须先使用LoadConf载入配置
            UPOPSrv.LoadConf(configPath);

            // 使用Post过来的内容构造SrvResponse
            SrvResponse resp = new SrvResponse(Util.NameValueCollection2StrDict(http.Request.Form));
            //订单编号
            string orderNumber = resp.Fields["orderNumber"];
            //订单支付金额
            double payMoney = Math.Round(Convert.ToDouble(AlipayConfig.MoneyFormatDco(resp.Fields["orderAmount"].ToString())), 2);

            UPOPReturlPara upa = new UPOPReturlPara();
            upa.isSuccessPay = (resp.ResponseCode == SrvResponse.RESP_SUCCESS) ? true : false;
            upa.orderNumber = orderNumber;
            upa.payMoney = payMoney;

            return upa;
        }        
    }

    public class UPOPPara
    {
        public string orderNumber { get; set; }
        /// <summary>
        /// 商品展示页
        /// </summary>
        public string showProUrl { get; set; }
        /// <summary>
        /// 商品展示名称
        /// </summary>
        public string proShowName { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string count { get; set; }
        /// <summary>
        /// 交易总金额
        /// </summary>
        public string total { get; set; }
        /// <summary>
        /// 前台通知页面
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 后台异步返回页面
        /// </summary>
        public string notifyUrl { get; set; }
    }

    public class UPOPReturlPara
    {
        /// <summary>
        /// 交易返回通知状态
        /// </summary>
        public bool isSuccessPay { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public double payMoney { get; set; }
        /// <summary>
        /// 商户交易订单号
        /// </summary>
        public string orderNumber { get; set; }
    }
}
