using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using WCF.BLL;
using WCF.Common.Tools;
using WCF.Inspector;
using WCF.Model.Data;
using WCF.Model.Service;
using WCF.Model.Service.ouput;
using WCF.Service.Interface;
using WCF.Service.MSMQ;
using WCF.Service.MSMQ.QueueService.Order;

namespace WCF.Service.Control
{
    public class MsmqTest_Service : IMsmq_Service
    {
        /// <summary>
        /// 提交订单消息
        /// </summary>
        /// <returns></returns>
        public Response<string> InsertOrder()
        {
            var ran = new Random();
            new QueueOrder().Send(new Order_Ms { OrderNumber = UtilsHelper.Cre_OrerNumBer(), OrderTotal = ran.Next(100, 9999) });
            return new Response<string>("订单提交成功");
        }

        /// <summary>
        /// 创建订单消息队列
        /// 说明：这里只是示范创建  生产操作 是手动创建队列
        /// </summary>
        /// <returns></returns>
        public Response<string> CreateQueueOrder()
        {
            new QueueBase("", 0).Createqueue(ConfigHelper.GetAppSettingsString("QueueOrderPath"));
            return new Response<string>("队列创建成功");
        }

    }
}
