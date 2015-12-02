﻿using System;
using System.Collections.Generic;
using System.Text;


public    class ClearPayDataTest
    {
         public static string testClearPayData()
        {
            string startdate = "2014-03-07";//开始时间
            string enddate = "2014-03-07";//结束时间

            YJPay yjpay = new YJPay();
            //调用sdk请求一键支付接口
            string res = yjpay.getClearPayData(startdate, enddate);

            //日志字符串
            StringBuilder logsb = new StringBuilder();
            logsb.Append(DateTime.Now.ToString() + "\n");

            logsb.Append("调用消费清算对账单接口，易宝返回结果为：" + res);

            return logsb.ToString();
        }
    
    }

