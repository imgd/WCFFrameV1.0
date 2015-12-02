using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public    class TestSuit
    {
    public static void testPay()
        {
            //AES算法示例
            TestAES.testAES();

            //RSA算法示例
            TestRSA.testRSA();

            //PC端网页支付
            //PCPayTest.testPay();

            //移动终端网页支付
            //MobilePayTest.testPay();    
      
            //绑卡支付示例
            BindPayTest.testBindPay();

            //支付结果查询
            QueryPayResultTest.testQueryPayResult();

            //获取绑卡关系列表
            BindListTest.testBindList();

            //根据银行卡卡号检查银行卡是否可以使用一键支付
            BankcardCheckTest.testBankcardCheck();

            //解绑
            UnBindTest.testUnBind();

            //商户通用接口——退款
            DirectFundTest.testDirectRefund();

            //商户通用接口——订单查询
            QueryPayInfoTest.testQueryPayInfo();

            //商户通用接口——退货退款查询
            QueryRefundInfoTest.testQueryRefundInfo();

            //商户通用接口——获取消费清算对账单
            //ClearPayDataTest.testClearPayData();

            //商户通用接口——获取退款清算对账单
            //ClearRefundDataTest.testClearRefundData();

            //商户的接收易宝支付通知回调接口中必须包括data和encryptkey两个参数，且请求方式为post
            //支付通知回调接口中返回的data和encryptkey的解析与其他接口对易宝支付返回结果的解析相同，可以直接调用YJPayUtil.checkYbCallbackResult方法

        }
    }

