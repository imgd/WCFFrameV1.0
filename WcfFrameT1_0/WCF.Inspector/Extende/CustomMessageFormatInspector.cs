using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Inspector
{
    /// <summary>
    /// 反/序列化和消息包装压缩 
    /// </summary>
    public class CustomMessageFormatInspector : IDispatchMessageFormatter
    {
        /// <summary>
        /// 将消息反序列化为参数数组
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        public void DeserializeRequest(System.ServiceModel.Channels.Message message, object[] parameters)
        {
            
        }

        /// <summary>
        /// 从指定的消息版本、参数数组和返回值序列化答复消息
        /// </summary>
        /// <param name="messageVersion"></param>
        /// <param name="parameters"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public System.ServiceModel.Channels.Message SerializeReply(System.ServiceModel.Channels.MessageVersion messageVersion, object[] parameters, object result)
        {
            return null;
        }
    }
}
