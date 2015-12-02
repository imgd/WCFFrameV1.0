using System;
using System.Collections.Generic;
using System.Text;

    /// <summary>
    /// 退款示例代码
    /// </summary>
public    class DirectFundTest
    {
        public static string testDirectRefund()
        {
            
            //商户退货订单号
            string orderid = "12345671509231900";
            //原来易宝支付交易订单号
            string origyborderid = "411307105873753738";
            //退款金额，单位：分
            int amount = 2;
            //币种
            int currency = 156;
            //退款原因
            string cause = "不需要了！！";

            YJPay yjpay = new YJPay();
            //调用sdk请求一键支付接口
            string res = yjpay.directRefund(orderid,origyborderid,amount,currency,cause);

            //日志字符串
            StringBuilder logsb = new StringBuilder();
            logsb.Append(DateTime.Now.ToString() + "\n");

            logsb.Append("调用退款接口，易宝返回结果为：" + res);

            return logsb.ToString();
           
        }
    }

