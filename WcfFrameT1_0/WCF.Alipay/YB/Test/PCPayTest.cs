using System;
using System.Collections.Generic;
using System.Text;
using System.Web;


    /// <summary>
    /// PC端网页支付示例代码
    /// </summary>
public    class PCPayTest
    {
        public static void testPay()
        {
            //一键支付URL前缀
            string apiprefix = APIURLConfig.payWebPrefix;

            //网页支付地址
            string pcPayURI = APIURLConfig.pcwebURI;

            //商户账户编号
            string merchantAccount = Config.merchantAccount;

            //商户公钥（该商户公钥需要在易宝一键支付商户后台报备）
            string merchantPublickey = Config.merchantPublickey;

            //商户私钥（商户公钥对应的私钥）
            string merchantPrivatekey = Config.merchantPrivatekey;

            //易宝支付分配的公钥（进入商户后台公钥管理，报备商户的公钥后分派的字符串）
            string yibaoPublickey = Config.yibaoPublickey;

            //随机生成商户AESkey
            string merchantAesKey = AES.GenerateAESKey();


            Random rnd = new Random();
            int n = rnd.Next(10000, 99999);
            //n就是你要的随机数，如果你要5位的就将上面改成(10000,99999),6位：(100000,999999)
           


            int amount = 2;//支付金额为分
            int currency = 156;
            string identityid = "1234567" + n.ToString();//用户身份标识
            int identitytype = 0;
            Random ra = new Random();
            string orderid = "1234567"+n.ToString();
            int orderexpdate = 60;
            int terminaltype = 3;
            String terminalid = "05-16-DC-59-C2-34";
            string productcatalog = "1";//商品类别码，商户支持的商品类别码由易宝支付运营人员根据商务协议配置
            string productdesc = "我叫MT";
            string productname = "符石";

            DateTime t1 = DateTime.Now;
            DateTime t2 = new DateTime(1970, 1, 1);
            double t = t1.Subtract(t2).TotalSeconds;
            int transtime = (int)t;

            string userip = "172.18.66.218";
            //商户提供的商户后台系统异步支付回调地址
            string callbackurl = "http://172.18.66.107:8082/payapi-java-demo/callback";
            //商户提供的商户前台系统异步支付回调地址
            string fcallbackurl = "http://172.18.66.107:8082/payapi-java-demo/fcallback";
            //用户浏览器ua
            string userua = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1";
           

            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("merchantaccount", merchantAccount);
            sd.Add("amount", amount);
            sd.Add("currency", currency);
            sd.Add("identityid", identityid);
            sd.Add("identitytype", identitytype);
            sd.Add("orderid", orderid);
            sd.Add("orderexpdate", orderexpdate);
            sd.Add("terminaltype", terminaltype);
            sd.Add("terminalid", terminalid);
            sd.Add("productcatalog", productcatalog);
            sd.Add("productdesc", productdesc);
            sd.Add("productname", productname);
            sd.Add("transtime", transtime);
            sd.Add("userip", userip);
            sd.Add("callbackurl", callbackurl);
            sd.Add("fcallbackurl", fcallbackurl);
            sd.Add("userua", userua);

            //生成RSA签名
            string sign = EncryptUtil.handleRSA(sd, merchantPrivatekey);
            Console.WriteLine("生成的签名为：" + sign);

            sd.Add("sign",sign);

            //将网页支付对象转换为json字符串
            string wpinfo_json = Newtonsoft.Json.JsonConvert.SerializeObject(sd);
            Console.WriteLine("网页支付明文数据json格式为：" + wpinfo_json);
            string datastring = AES.Encrypt(wpinfo_json, merchantAesKey);
            Console.WriteLine("网页支付业务数据经过AES加密后的值为：" + datastring);

            //将商户merchantAesKey用RSA算法加密
            Console.WriteLine("merchantAesKey为：" + merchantAesKey);
            string encryptkey = RSAFromPkcs8.encryptData(merchantAesKey, yibaoPublickey, "UTF-8");
            Console.WriteLine("encryptkey为：" + encryptkey);

            //打开浏览器访问一键支付网页支付链接地址，请求方式为get
            string postParams = "data=" +HttpUtility.UrlEncode(datastring) + "&encryptkey=" + HttpUtility.UrlEncode(encryptkey) + "&merchantaccount=" + merchantAccount;
            string url = apiprefix + pcPayURI + "?" + postParams;
            
            Console.WriteLine("网页支付链接地址为：" + url);
            Console.WriteLine("网页支付链接地址长度为：" + url.Length);
            System.Diagnostics.Process.Start("firefox.exe", url);//打开firefox浏览器
            System.Diagnostics.Process.Start(url);

            Console.ReadLine();
        }
    }

