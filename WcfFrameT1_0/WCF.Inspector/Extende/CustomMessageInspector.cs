using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Inspector
{
    /// <summary>
    /// 客户端行为操作扩展
    /// </summary>
    public class CustomMessageInspector : IDispatchMessageInspector
    {
        /// <summary>
        /// 客户端请求发送到服务端执行之前 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <param name="instanceContext"></param>
        /// <returns></returns>
        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            //验证用户的身份
            if (IsIdentityPass())
            {
                return "客户端请求错误：909，身份验证失败，没有权限访问。";
            }
            else
            {
                
                //request.WriteMessage(
                throw new FaultException("客户端请求错误：909，身份验证失败，没有权限访问。", new FaultCode("909 DivideByZeroFault"));
            }
        }

        /// <summary>
        /// 服务端相应发送到客户端之前
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {

        }

        /// <summary>
        /// REST 身份验证 
        /// </summary>
        /// <returns></returns>
        private bool IsIdentityPass()
        {
            string ClientTokenName = "token";
            bool IsIdentityPass = false;
            if (OperationContext.Current == null)
            {
                throw new FaultException("WCF配置错误，OperationInterceptorClass 特性只能配置REST服务。请修改。");
            }

            WebOperationContext context = new WebOperationContext(OperationContext.Current);
            var headers = context.IncomingRequest.Headers;
            if (headers != null)
            {
                string ClientToken = headers[ClientTokenName];
                if (new BaseClientCheckFactory(ClientToken)
                    .ClientIdentityCheck())
                {
                    IsIdentityPass = true;
                }
            }

            return IsIdentityPass;
        }
    }
}
