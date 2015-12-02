using System;
using System.Collections.Generic;
using System.Text;


public    class BindListTest
    {
         public static string testBindList()
        {
            string identityid = "493002407599521";//用户身份标识
			int identitytype = 0;

            YJPay yjpay = new YJPay();
            //调用sdk请求一键支付接口
            string res = yjpay.getBindList(identityid,identitytype);

            //日志字符串
            StringBuilder logsb = new StringBuilder();
            logsb.Append(DateTime.Now.ToString() + "\n");
            
            logsb.Append("调用获取绑卡关系列表接口，易宝返回结果为：" + res);

            return logsb.ToString();
        }
    
    }

