using System;
using System.Collections.Generic;
using System.Text;

    /// <summary>
    /// 绑卡支付示例代码，绑卡支付之前请先通过获取绑卡关系列表获取有效的绑卡关系
    /// </summary>
public    class BindPayTest
    {
       public static string testBindPay()
        {
            int amount =  2;//支付金额为分
			string bindid = "312";
			int currency = 156;//币种，默认为人民币
            string identityid = "493002407599521Q";//用户身份标识
			int identitytype = 0;//用户身份标识类型
            Random ra = new Random();
			string orderid = "1234567" + 50 * ra.Next();//商户交易订单号
            string other = "05-16-DC-59-C2-34";// 终端硬件标识，MAC地址或者IMEI
			string productcatalog = "1";//商品类别码，商户支持的商品类别码由易宝支付运营人员根据商务协议配置
			string productdesc = "我叫MT";//商品名称
			string productname = "符石";//商品描述

            DateTime t1 = DateTime.Now;
            DateTime t2 = new DateTime(1970, 1, 1);
            double t = t1.Subtract(t2).TotalSeconds;
            int transtime = (int)t;//交易时间

			string userip = "172.18.66.218";
            //商户提供的商户后台系统异步支付回调地址
			string callbackurl = "http://172.18.66.107:8082/payapi-java-demo/callback";
            //商户提供的商户前台系统异步支付回调地址
            string fcallbackurl = "http://172.18.66.107:8082/payapi-java-demo/fcallback";

            int terminaltype = 1;
            String terminalid = "00-10-5C-AD-72-E3";

            YJPay yjpay = new YJPay();
            //step1:调用sdk请求一键支付接口
            string payres = yjpay.bindPayRequest(bindid, amount, currency, identityid, identitytype, orderid,other,
                productcatalog,productdesc,productname,transtime,userip,callbackurl,fcallbackurl,terminaltype,terminalid);

            //日志字符串
            StringBuilder logsb = new StringBuilder();
            logsb.Append(DateTime.Now.ToString() + "\n");
            
            logsb.Append("易宝绑卡支付请求接口返回结果为：" + payres+"\n");

            //将支付请求获得的易宝返回结果反序列化
            SortedDictionary<string, object> payresSD =(SortedDictionary<string, object>) Newtonsoft.Json.JsonConvert.DeserializeObject(payres, typeof(SortedDictionary<string, object>));
            
           //获取是否需要短信校验的建议
            string s = payresSD["smsconfirm"].ToString();
            
           if(s=="0"){
                //step2:易宝建议无需短信校验，直接调用确认支付接口
                string confirmpayjson=yjpay.confirmPay(orderid, null);
                logsb.Append("易宝建议无需短信校验，直接调用确认支付接口返回的结果：" + confirmpayjson+"\n");
            }
            else if (s == "1")
            {
                //step2:易宝建议需要短信校验，调用易宝向用户发生短信校验接口。用户在商户的交互页面输入验证码后才能调用确认支付接口
                string validatecodejson=yjpay.sendValidateCode(orderid);
                logsb.Append("易宝建议需要短信校验，调用发送短信验证码接口返回的结果：" + validatecodejson+"\n");
                //step3:确认支付
                string confirmpayjson2=yjpay.confirmPay(orderid, "123456");
                logsb.Append("易宝建议需要短信校验，调用确认支付接口返回的结果：" + confirmpayjson2+"\n");
            }

           return logsb.ToString();
        }
    }

