using System;
using System.Collections.Generic;
using System.Text;

    /// <summary>
    /// 支付结果查询示例代码
    /// </summary>
public    class QueryPayResultTest
    {
       public static string testQueryPayResult()
        {
            
            //商户交易订单号
            string orderid = "02113122311175800082_1387768678";

            YJPay yjpay = new YJPay();
            //调用sdk请求一键支付接口
            string res = yjpay.queryPayResult(orderid);

            //日志字符串
            StringBuilder logsb = new StringBuilder();
            logsb.Append(DateTime.Now.ToString() + "\n");

            logsb.Append("调用支付结果查询接口，易宝返回结果为：" + res);

            return logsb.ToString();
        }
    }

