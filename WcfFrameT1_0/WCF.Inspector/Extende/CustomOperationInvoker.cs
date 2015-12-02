using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Inspector.Extende
{
    /// <summary>
    /// 方法执行检查器
    /// </summary>
    public class CustomOperationInvoker : IOperationInvoker
    {

        
        IOperationInvoker m_OldInvoker;

        public CustomOperationInvoker(IOperationInvoker invoker)
        {
            m_OldInvoker = invoker;
        }

        /// <summary>
        /// 扩展自定义检查
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="inputs"></param>
        /// <param name="outputs"></param>
        /// <returns></returns>
        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            object result = m_OldInvoker.Invoke(instance, inputs, out outputs);
            return result;
        }
       

        public object[] AllocateInputs()
        {
            return m_OldInvoker.AllocateInputs();
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            return m_OldInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {

            return m_OldInvoker.InvokeEnd(instance, out outputs, result);

        }

        public bool IsSynchronous
        {
            get
            {
                return m_OldInvoker.IsSynchronous;
            }
        }
    }
}
