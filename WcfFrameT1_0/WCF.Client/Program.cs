using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.Common.Tools;
using Extensions.ConvertExtension;
using System.Threading;
using System.Collections;
using System.Transactions;
using WCF.Service.MSMQ.QueueService.Order;
using WCF.Service.MSMQ;

namespace WCF.Client
{
    /// <summary>
    /// 订单消息处理
    /// </summary>
    class Program
    {
        private static int transactionTimeout = ConfigHelper.GetAppSettingsString("TransactionTimeout").ParseInt();
        private static int queueTimeout = ConfigHelper.GetAppSettingsString("QueueTimeout").ParseInt();
        private static int batchSize = ConfigHelper.GetAppSettingsString("BatchSize").ParseInt();
        private static int threadCount = ConfigHelper.GetAppSettingsString("ThreadCount").ParseInt();
        private static int totalOrdersProcessed = 0;

        static void Main(string[] args)
        {
            new QueueBase(string.Empty, 1).Deletequeue(@".\Private$\PSOrders");
            //ThreadRun();
        }

        private static void ThreadRun()
        {
            Thread workTicketThread;
            Thread[] workerThreads = new Thread[threadCount];


            for (int i = 0; i < threadCount; i++)
            {
                workTicketThread = new Thread(new ThreadStart(ProcessOrders));
                //指示线呈是否为一后台线程
                workTicketThread.IsBackground = true;
                workTicketThread.SetApartmentState(ApartmentState.STA);

                workTicketThread.Start();
                workerThreads[i] = workTicketThread;
            }

            Console.WriteLine("开始处理,按任意键停止.");
            Console.ReadLine();
            Console.WriteLine("正在终止线程,请等待......");

            //终止所以线程
            for (int i = 0; i < workerThreads.Length; i++)
            {
                workerThreads[i].Abort();
            }

            Console.WriteLine();
            Console.WriteLine(totalOrdersProcessed + " 张订单已经处理.");
            Console.WriteLine("已终止处理.按任意键退出......");
            Console.ReadLine();
        }

        /// <summary>
        /// 处理订单事宜
        /// </summary>
        private static void ProcessOrders()
        {
            //计算当前线程当前批次处理的事务超时总时间
            TimeSpan tsTimeout = TimeSpan.FromSeconds(Convert.ToDouble(transactionTimeout * batchSize));
            QueueOrder order = new QueueOrder();

            while (true)
            {
                ///开始事务的时间间隔节点
                TimeSpan datatimeStarting = new TimeSpan(DateTime.Now.Ticks);
                //单次获取订单所得时间
                double elapsedTime = 0;
                //订单处理计数
                int processedItems = 0;
                //当前批次处理订单集合
                ArrayList queueOrders = new ArrayList();

                //首先验证事务
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tsTimeout))
                {
                    //从队列中检索订单
                    for (int i = 0; i < batchSize; i++)
                    {
                        try
                        {
                            //在一定时间 类接收队列的订单
                            if ((elapsedTime + queueTimeout + transactionTimeout) < tsTimeout.TotalSeconds)
                            {
                                queueOrders.Add(order.Receive(queueTimeout));
                            }
                            else
                            {
                                i = batchSize;  // 结束循环
                            }
                            elapsedTime = new TimeSpan(DateTime.Now.Ticks).TotalSeconds - datatimeStarting.TotalSeconds;
                        }
                        catch (TimeoutException)
                        {
                            //没有可以等待的消息也结束循环
                            i = batchSize;
                        }
                    }

                    //处理队列的订单
                    for (int k = 0; k < queueOrders.Count; k++)
                    {
                        //根据业务逻辑处理
                        //处理代码
                        processedItems++;
                        totalOrdersProcessed++;
                    }
                    //处理完毕或者是超时
                    ts.Complete();
                }
                Console.WriteLine("(Thread Id " + Thread.CurrentThread.ManagedThreadId + ") batch finished, " + processedItems + " items, in " + elapsedTime.ToString() + " seconds.");

            }
        }
    }
}
