using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Service.MSMQ
{
    public class QueueBase : IDisposable
    {
        /// <summary>
        /// 事务型队列
        /// </summary>
        protected MessageQueueTransactionType _transactionType = MessageQueueTransactionType.Automatic;
        protected MessageQueue _queue;  //消息队列
        protected TimeSpan _timeout;    //时间间隔

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="queuePath"></param>
        /// <param name="timeoutSeconds"></param>
        public QueueBase(string queuePath, int timeoutSeconds)
        {
            _queue = new MessageQueue(queuePath);
            _timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeoutSeconds));

            _queue.DefaultPropertiesToSend.AttachSenderId = false;
            _queue.DefaultPropertiesToSend.UseAuthentication = false;
            _queue.DefaultPropertiesToSend.UseEncryption = false;
            _queue.DefaultPropertiesToSend.AcknowledgeType = AcknowledgeTypes.None;
            _queue.DefaultPropertiesToSend.UseJournalQueue = false;
        }

        /// <summary>
        /// 接收消息方法
        /// </summary>
        public virtual object Receive()
        {
            try
            {
                using (Message message = _queue.Receive(_timeout, _transactionType))
                    return message;
            }
            catch (MessageQueueException mqex)
            {
                if (mqex.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
                    throw new TimeoutException();
                throw;
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public virtual void Send(object msg)
        {
            _queue.Send(msg, _transactionType);
        }


        /// <summary>
        /// 通过创建本地消息队列
        /// </summary>
        /// <param name="queuePath"></param>
        public void Createqueue(string queuePath)
        {
            try
            {
                if (!MessageQueue.Exists(queuePath))
                {
                    MessageQueue.Create(queuePath);
                }
            }
            catch (MessageQueueException e)
            {
                throw e;
            }
        }

        public void Deletequeue(string path)
        {
            if (MessageQueue.Exists(path))
            {
                MessageQueue.Delete(path);
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _queue.Dispose();
        }
    }
}
