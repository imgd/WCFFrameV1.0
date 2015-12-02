using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF.Model.Service;

namespace WCF.Service.MSMQ.QueueService.Order
{
    interface IQueueOrder
    {
        /// <summary>
        /// 发送订单消息
        /// </summary>
        /// <param name="order"></param>
        void Send(Order_Ms order);

        /// <summary>
        /// 接收订单消息
        /// </summary>
        /// <returns></returns>
        Order_Ms Receive();
        Order_Ms Receive(int timeoutSeconds);
    }
}
