using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using WCF.Model.Data;

namespace WCF.DAL
{
    public class EFContextFactory
    {
        /// <summary>
        /// 帮我们返回当前线程内的数据库上下文，如果当前线程内没有上下文，那么创建一个上下文，并保证
        /// 上线问实例在线程内部是唯一的
        /// </summary>
        /// <returns></returns>
        public static DbContext GetCurrentDbContext()
        {
            var dbContext = CallContext.GetData("DbContext") as DbContext;
            //线程在数据槽里面没有此上下文
            if (dbContext == null)  
            {
                //创建一个EF上下文
                dbContext = new Sucool_DB_Entities(); 
                CallContext.SetData("DbContext", dbContext);
            }
            return dbContext;
        }
    }
}
