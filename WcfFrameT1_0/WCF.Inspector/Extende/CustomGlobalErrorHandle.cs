using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using Extensions.LogExtension;

namespace WCF.Inspector
{
    /// <summary>
    /// 全局错误处理
    /// </summary>
    public class CustomGlobalErrorHandle : IErrorHandler
    {

        public bool HandleError(Exception error)
        {
            return false;
        }

        /// <summary>
        /// 自定义消息错误处理
        /// </summary>
        /// <param name="error"></param>
        /// <param name="version"></param>
        /// <param name="fault"></param>
        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            ///全局错误日志记录
            //string.Format("ERROR:{0}\r\n引起异常方法：{1}\r\n详细：{2}",
            //    OperationContext.Current.IncomingMessageHeaders.Action.ToString(),
            //    error.TargetSite.Name, error.Message).WriteLog("SystemErrorLog");

            var newEx = new FaultException(string.Format("ERROR：{0} ", error.Message));
            MessageFault msgFault = newEx.CreateMessageFault();
            fault = Message.CreateMessage(version, msgFault, newEx.Action);
        }
    }
}
