using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using WCF.Inspector;

namespace WCF.Model.Service.ouput
{

    /// <summary>
    /// 服务接口输出类 继承自身份验证类
    /// 说明：此类为服务数据输出专用 模型类  其他数据层请慎用
    /// 2015.11.13 改为特性注入 此类将抛弃 身份验证模块
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    [DataContract(Name = "results")]
    public class Response<T> : ClientIdentityCheck where T : class
    {

        /// <summary>
        /// 构造
        /// </summary>
        public Response() { IsPass = IsIdentityPass; }
        public Response(T data)
        {
            IsPass = IsIdentityPass;
            SetParas(200, "请求成功", data);
        }
        public Response(int code, string description)
        {
            IsPass = IsIdentityPass;
            SetParas(code, description, null);
        }
        public Response(int code, string description, T data)
        {
            IsPass = IsIdentityPass;
            SetParas(code, description, data);
        }

        /// <summary>
        /// 设置属性值
        /// </summary> 
        public void SetResultData(int code, string description)
        {
            SetParas(code, description, null);
        }
        public void SetResultData(int code, string description, T data)
        {
            SetParas(code, description, data);
        }



        /// <summary>
        /// 返回的状态码
        /// </summary>
        [DataMember(Name = "code")]
        public int ResultDataCode { get; set; }
        /// <summary>
        /// 返回状态码的描述信息
        /// </summary>
        [DataMember(Name = "description")]
        public string ResultDataCodeDescription { get; set; }
        /// <summary>
        /// 返回的结果集
        /// </summary>
        [DataMember(Name = "data")]
        public T ResultData { get; set; }

        /// <summary>
        /// 是否身份通过
        /// </summary>
        private bool IsPass { get; set; }
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="code">结果编号</param>
        /// <param name="description">结果描述</param>
        /// <param name="data">返回结果值</param>
        private void SetParas(int code, string description, T data)
        {
            if (IsPass)
            {
                this.ResultDataCode = code;
                this.ResultDataCodeDescription = description;
                this.ResultData = data;
            }
            else
            {
                this.ResultDataCode = -9999;
                this.ResultDataCodeDescription = "身份验证不通过";
                this.ResultData = null;
            }
        }
    }
}
