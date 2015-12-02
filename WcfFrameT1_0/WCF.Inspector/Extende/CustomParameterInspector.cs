using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Inspector.Extende
{
    /// <summary>
    /// 组定义参数检查器
    /// </summary>
    public class CustomParameterInspector : IParameterInspector
    {
        /// <summary>
        /// 在客户端调用返回之后、服务响应发送之前调用
        /// </summary>
        /// <param name="operationName"></param>
        /// <param name="outputs"></param>
        /// <param name="returnValue"></param>
        /// <param name="correlationState"></param>
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            
        }

        /// <summary>
        /// 在发送客户端调用之前、服务响应返回之后调用
        /// </summary>
        /// <param name="operationName"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public object BeforeCall(string operationName, object[] inputs)
        {
            return null;
        }
    }
}
