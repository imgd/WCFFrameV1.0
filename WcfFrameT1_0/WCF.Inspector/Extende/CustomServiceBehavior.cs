using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Inspector
{
    /// <summary>
    /// 自定义总结点行为扩展
    /// </summary>
    public class CustomServiceBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            //自定义消息处理
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new CustomMessageInspector());
            //自定义错误处理
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(new CustomGlobalErrorHandle());
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            
        }
    }
}
