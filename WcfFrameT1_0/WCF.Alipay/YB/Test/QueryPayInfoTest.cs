using System;
using System.Collections.Generic;
using System.Text;

    /// <summary>
    /// 商户通用接口——交易订单查询——示例
    /// </summary>
 public   class QueryPayInfoTest
    {
        public static string testQueryPayInfo()
        {
            //商户交易订单号
            string orderid = "02113122311175800082_1387768678";
            string yborderid = null;

            YJPay yjpay = new YJPay();
            //调用sdk请求一键支付接口
            string res = yjpay.queryPayOrderInfo(orderid,yborderid);
            //日志字符串
            StringBuilder logsb = new StringBuilder();
            logsb.Append(DateTime.Now.ToString() + "\n");

            logsb.Append("调用交易订单查询接口，易宝返回结果为：" + res);

            return logsb.ToString();

        }
    }

