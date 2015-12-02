using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Inspector
{
    /// <summary>
    /// 自定义扩展标记 【类】
    /// 包含身份验证和异常捕获日志记录
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]    
    public class OperationInterceptorClassAttribute : Attribute, IServiceBehavior
    {
        /// <summary>
        /// 参数行为扩展
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        /// <param name="endpoints"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            
        }
        /// <summary>
        /// 自定义扩展
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            var points = serviceDescription.Endpoints;
            foreach (ServiceEndpoint endpoint in points)
            {
                //添加我的终结点运行时自定义行为
                endpoint.Behaviors.Add(new CustomServiceBehavior());

                var behaviorattrbute = endpoint.Contract.Operations;
                //添加我的操作运行时自定义行为
                foreach (OperationDescription opera in behaviorattrbute)
                {
                    if (opera.Behaviors.Find<OperationInterceptorAttribute>() != null)
                    {
                        continue;
                    }
                    opera.Behaviors.Add(new OperationInterceptorAttribute());
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            
        }
    }
}
