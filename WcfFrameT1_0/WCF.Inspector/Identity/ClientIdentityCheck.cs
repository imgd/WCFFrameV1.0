using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using Extensions.ConvertExtension;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace WCF.Inspector
{
    [DataContract]
    [Serializable]
    public class ClientIdentityCheck : BaseClientIdentityCheck
    {

        public override void CheckIdentity()
        {
            ClientTokenName = "token";
            IsIdentityPass = false;
            if (OperationContext.Current == null)
            {
                throw new FaultException("WCF配置错误，OperationInterceptorClass 特性只能配置REST服务。请修改。");
            }

            WebOperationContext context = new WebOperationContext(OperationContext.Current);
            var headers = context.IncomingRequest.Headers;
            if (!headers.IsNull())
            {
                ClientToken = headers[ClientTokenName];

                if (new BaseClientCheckFactory(ClientToken)
                    .ClientIdentityCheck())
                {
                    IsIdentityPass = true;
                }
            }
        }
    }
}
