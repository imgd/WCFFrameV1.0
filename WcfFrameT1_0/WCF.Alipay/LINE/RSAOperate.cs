﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Specialized;
using System.Text;
using System.Net;
using System.IO;
namespace Com.Alipay
{
    public class RSAOperate
    {
        public RSAOperate(){}
        /// <summary>
        /// 对比报文是否为安全
        /// </summary>
        /// <param name="sign">加密后RSA</param>
        /// <param name="RSAChar">加密前RSA</param>
        /// <returns>true安全false不匹配</returns>
        public bool GetIsSafty(string Sign,string RSAChar)
        {
            if (Sign == RSASign.GetMD5RSA(RSAChar))
                return true;
            else
                return false;
        }
        /// <summary>
        /// 返回访问参数串队
        /// </summary>
        /// <param name="WinFormQuery">表格post之后传输页面Form值</param>
        /// <returns></returns>
        public string GetUrlParamString(NameValueCollection WinFormQuery, string[] PayParam)
        {
            // 得到支付类排序后参数集合
            StringBuilder _sbuilder = new StringBuilder();
            for (int parindex = 0; parindex < PayParam.Length; parindex++)
            {

                    if (parindex == 0)
                    {
                        if (PayParam[parindex].ToString() == "merchantCode")
                            _sbuilder.Append(PayParam[parindex].ToString() + "=" + ProperConst.merchantCode);
                        else
                            _sbuilder.Append(PayParam[parindex].ToString() + "=" + WinFormQuery[PayParam[parindex].ToString()]);
                    }
                    else
                    {
                        if (PayParam[parindex].ToString() == "merchantCode")
                            _sbuilder.Append(("&" + PayParam[parindex].ToString()) + "=" + ProperConst.merchantCode);
                        else
                            _sbuilder.Append(("&" + PayParam[parindex].ToString()) + "=" + WinFormQuery[PayParam[parindex].ToString()]);
                    }
            }
            return _sbuilder.ToString();
        }
        /// <summary>
        /// 返回访问参数串队
        /// </summary>
        /// <param name="WinFormQueryJson">表格post之后传输页面Form值</param>
        /// <returns></returns>
        public string GetUrlParamString(string WinFormQueryJson, string[] PayParam)
        {
            // 得到支付类排序后参数集合
            StringBuilder _sbuilder = new StringBuilder();
            for (int parindex = 0; parindex < PayParam.Length; parindex++)
            {

                if (parindex == 0)
                {
                    if (PayParam[parindex].ToString() == "merchantCode")
                        _sbuilder.Append(PayParam[parindex].ToString() + "=" + ProperConst.merchantCode);
                    else
                        _sbuilder.Append(PayParam[parindex].ToString() + "=" + GetjosnValue(WinFormQueryJson,PayParam[parindex].ToString()));
                }
                else
                {
                    if (PayParam[parindex].ToString() == "merchantCode")
                        _sbuilder.Append(("&" + PayParam[parindex].ToString()) + "=" + ProperConst.merchantCode);
                    else
                        _sbuilder.Append(("&" + PayParam[parindex].ToString()) + "=" + GetjosnValue(WinFormQueryJson, PayParam[parindex].ToString()));
                }
            }
            return _sbuilder.ToString();
        }
        /// <summary>
        /// 返回传输到服务器的固定格式Json字符串
        /// </summary>
        /// <param name="WinFormQuery"></param>
        /// <param name="ArrayParam"></param>
        /// <param name="MD5Key">加密密文</param>
        /// <returns></returns>
        public string GetPostJson(NameValueCollection WinFormQuery, string[] ArrayParam,string MD5Key)
        {
            string _jsonhead = "{\"param\":{";
            string _jsonbottom = "\"sign\":\"" + MD5Key + "\"},\"" + ProperConst.project_id + "\":\"" + ProperConst.project_idValue + "\"}";
            for (int i = 0; i < ArrayParam.Length; i++)
            {
                if (ArrayParam[i].ToString() == "merchantCode")
                    _jsonhead += "\"" + ArrayParam[i] + "\":\"" + ProperConst.merchantCode + "\",";
                else if (ArrayParam[i].ToString() == "amount" || ArrayParam[i].ToString() == "totalamount")//金额为整数
                    _jsonhead += "\"" + ArrayParam[i] + "\":" + WinFormQuery[ArrayParam[i]] + ",";
                else
                    _jsonhead += "\"" + ArrayParam[i] + "\":\"" + WinFormQuery[ArrayParam[i]] + "\",";
            }
            _jsonhead += _jsonbottom;
            return _jsonhead;
        }
        /// <summary>
        /// Web后台Post访问
        /// </summary>
        /// <param name="WebUrl">访问连接地址</param>
        /// <param name="ParamJson">此处为固定格式Json字符串</param>
        /// <returns></returns>
        public string GetPostWeb(string WebUrl,string ParamJson)
        {
            //此处为后台构建接口访问并且返回订单结果
            //用户若不使用此方法，也可使用Ajax的Post方法返回订单信息
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(ParamJson);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(WebUrl); //发送地址
            webRequest.Method = "POST";//提交方式
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = byteArray.Length;
            Stream newStream = webRequest.GetRequestStream(); // Send the data.
            newStream.Write(byteArray, 0, byteArray.Length); //写入参数
            newStream.Close();
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();//获取响应
            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            return sr.ReadToEnd(); // 返回的数据
        }
        /**************************************************************************
         ********************此方法为本人所写获取Json的值，用户可以采用其他Json组建处理Json************Json.NET*******
         ***************************************************************************/
        /// <summary>
        /// 获取json中字段的值
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <param name="Key">关键字</param>
        /// <returns></returns>
        public string GetjosnValue(string json, string Key)
        {
            string Value = "";
            int KeyIndex = 0;//关键字索引
            KeyIndex = json.IndexOf(Key);
            if (KeyIndex == -1) return "";
            string Cutjson = "";
            if (Key.ToLower() == "amount" || Key.ToLower() == "totalamount")//整数类型特殊处理
            {
                Cutjson = json.Substring((Key.Length + KeyIndex + 2), json.Length - (Key.Length + KeyIndex + 2));
                Value = Cutjson.Substring(0, Cutjson.IndexOf(","));
            }
            else
            {
                Cutjson = json.Substring((Key.Length + KeyIndex + 3), json.Length - (Key.Length + KeyIndex + 3));
            Value = Cutjson.Substring(0,Cutjson.IndexOf("\""));
            }
            return Value;
        }
        /// <summary>
        /// 根据json字符串返回对应参数队
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <param name="Key">关键字</param>
        /// <returns></returns>
        public string GetParamJosnString(string json, string[] param)
        {
            string ReValue = "";
            for (int i = 0; i < param.Length; i++)
            {
                ReValue += param[i].ToString() + "=" + GetjosnValue(json, param[i].ToString()) + "&";
            }
            if (ReValue.Length > 0)
                ReValue = ReValue.Substring(0, ReValue.Length - 1);
            return ReValue;
        }
    }
}
