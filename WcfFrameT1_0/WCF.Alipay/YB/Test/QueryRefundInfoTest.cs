using System;
using System.Collections.Generic;
using System.Text;


    /// <summary>
    /// 商户通用接口——退货退款查询——示例
    /// </summary>
public    class QueryRefundInfoTest
    {
        public static string testQueryRefundInfo()
        {
 
            //商户退货订单号
            string orderid = "123456759834024";
            //原来易宝支付退款流水号
            string yborderid = "201306136121538840";

            YJPay yjpay = new YJPay();
            //调用sdk请求一键支付接口
            string res = yjpay.queryRefundOrder(orderid,yborderid);

            //日志字符串
            StringBuilder logsb = new StringBuilder();
            logsb.Append(DateTime.Now.ToString() + "\n");

            logsb.Append("调用查询退款订单信息接口，易宝返回结果为：" + res);

            return logsb.ToString();
 
        }
    }

