using System;
using System.Collections.Generic;
using System.Text;


public    class UnBindTest
    {
         public static void testUnBind()
        {
 
            string identityid = "493002407599521";//用户身份标识
			int identitytype = 0;
            string bindid = "739";

            YJPay yjpay = new YJPay();
            //调用sdk请求一键支付接口
            string res = yjpay.unbind(identityid,identitytype,bindid);

            Console.WriteLine("易宝返回结果为：" + res);

            Console.ReadLine();
        }
    
    }

