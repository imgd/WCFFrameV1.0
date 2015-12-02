using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Extensions.LogExtension;
using Sucool.InternetFace.Alipay;
using WCF.Alipay;

namespace WxPayAPI
{
    /// <summary>
    /// 支付结果通知回调处理类
    /// 负责接收微信支付后台发送的支付结果并对订单有效性进行验证，将验证结果反馈给微信支付后台
    /// </summary>
    public class ResultNotify : Notify
    {
        public ResultNotify(HttpContext page)
            : base(page)
        {
        }

        public WXPayBack ProcessNotify()
        {
            WXPayBack result = new WXPayBack() { ResultCode = 0, Msg = "获取请求成功" };

            SortedDictionary<string, object> paras;
            
            WxPayData notifyData = GetNotifyData(out paras);
            //string str = "WX支付异步返回成功";
            //foreach (var item in paras)
            //{
            //    str += string.Format("{0}={1},", item.Key, item.Value);
            //}
            //str.WriteLog();

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                Log.Error(this.GetType().ToString(), "The Pay result is error : " + res.ToXml());
                page.Response.Write(res.ToXml());
                //page.Response.End();

                result.SetPara(-1, "", "", "支付结果中微信订单号不存在");
                return result;

            }

            string transaction_id = notifyData.GetValue("transaction_id").ToString();

            //查询订单，判断订单真实性
            if (!QueryOrder(transaction_id))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                Log.Error(this.GetType().ToString(), "Order query failure : " + res.ToXml());
                page.Response.Write(res.ToXml());
                //page.Response.End();

                result.SetPara(-2, "", "", "订单查询失败");
            }
            //查询订单成功
            else
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "SUCCESS");
                res.SetValue("return_msg", "OK");
                Log.Info(this.GetType().ToString(), "order query success : " + res.ToXml());
                page.Response.Write(res.ToXml());
                //page.Response.End();

                result.SetPara(1, paras["attach"].ToString(),
                    AlipayConfig.MoneyFormatDco(paras["total_fee"].ToString()).ToString(), 
                    "查询订单成功");

                string.Format("dingdanhao:{0},jine:{1} zhifuchenggon ", paras["attach"].ToString(), AlipayConfig.MoneyFormatDco(paras["total_fee"].ToString()).ToString()).WriteLog();
            }

            return result;
        }
        public WXPayBack ProcessNotify(bool isApp)
        {
            WXPayBack result = new WXPayBack() { ResultCode = 0, Msg = "获取请求成功" };

            SortedDictionary<string, object> paras;

            WxPayData notifyData = GetNotifyData(out paras);
            //string str = "WX支付异步返回成功";
            //foreach (var item in paras)
            //{
            //    str += string.Format("{0}={1},", item.Key, item.Value);
            //}
            //str.WriteLog();

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                Log.Error(this.GetType().ToString(), "The Pay result is error : " + res.ToXml());
                page.Response.Write(res.ToXml());
                //page.Response.End();

                result.SetPara(-1, "", "", "支付结果中微信订单号不存在");
                return result;

            }

            string transaction_id = notifyData.GetValue("transaction_id").ToString();

            //查询订单，判断订单真实性
            if (!QueryOrder(transaction_id, true))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                Log.Error(this.GetType().ToString(), "Order query failure : " + res.ToXml());
                page.Response.Write(res.ToXml());
                //page.Response.End();

                result.SetPara(-2, "", "", "订单查询失败");
            }
            //查询订单成功
            else
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "SUCCESS");
                res.SetValue("return_msg", "OK");
                Log.Info(this.GetType().ToString(), "order query success : " + res.ToXml());
                page.Response.Write(res.ToXml());
                //page.Response.End();

                result.SetPara(1, paras["attach"].ToString(),
                    AlipayConfig.MoneyFormatDco(paras["total_fee"].ToString()).ToString(),
                    "查询订单成功");

                string.Format("dingdanhao:{0},jine:{1} zhifuchenggon ", paras["attach"].ToString(), AlipayConfig.MoneyFormatDco(paras["total_fee"].ToString()).ToString()).WriteLog();
            }

            return result;
        }

        public class WXPayBack
        {
            public int ResultCode { get; set; }
            public string OrderNumber { get; set; }
            public string TotalFell { get; set; }
            public string Msg { get; set; }

            public void SetPara(int resultCode, string orderNumber, string totalFell, string msg)
            {
                this.ResultCode = resultCode;
                this.OrderNumber = orderNumber;
                this.TotalFell = totalFell;
                this.Msg = msg;
            }
        }

        //查询订单
        private bool QueryOrder(string transaction_id)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = WxPayApi.OrderQuery(req);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool QueryOrder(string transaction_id,bool isapp)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = WxPayApi.OrderQuery(req,true);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}