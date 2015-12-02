using System;
using System.Collections.Generic;
using System.Text;


public    class BankcardCheckTest
    {
        public static void testBankcardCheck()
        {
            YJPay yjPay = new YJPay();

            //银行卡号
            string cardno = "3568891181175688";

            //调用sdk中的银行卡校验方法
            String viewYbResult = yjPay.bankCardCheck(cardno);
            StringBuilder logsb = new StringBuilder();
            logsb.Append(DateTime.Now.ToString() + "\n");
            logsb.Append("调用sdk中的银行卡校验方法,易宝返回的业务数据明文为：" + viewYbResult);
           
        }

    }

