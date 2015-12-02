using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using WCF.Common.Tools;
using WCF.Model.Service;

namespace WCF.Service.MSMQ.QueueService.Order
{
    /// <summary>
    /// 订单消息队列
    /// </summary>
    public class QueueOrder : QueueBase, IQueueOrder
    {
        private static readonly string queueOrderPath = ConfigHelper.GetAppSettingsString("QueueOrderPath");
        private static int defaultTimeOutSeconds = 20;

        public QueueOrder()
            : base(queueOrderPath, defaultTimeOutSeconds)
        {
            _queue.Formatter = new BinaryMessageFormatter();
        }

        public void Send(Model.Service.Order_Ms order)
        {
            base._transactionType = MessageQueueTransactionType.Single;
            base.Send(order);
        }

        public new Model.Service.Order_Ms Receive()
        {
            base._transactionType = MessageQueueTransactionType.Automatic;
            return (Order_Ms)((Message)base.Receive()).Body;
        }

        public Model.Service.Order_Ms Receive(int timeoutSeconds)
        {
            base._timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeoutSeconds));
            return Receive();
        }
    }
}
