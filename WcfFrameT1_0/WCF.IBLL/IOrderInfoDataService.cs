using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WCF.Model.Data;

namespace WCF.IBLL
{
    public  interface IOrderInfoDataService : IBaseService<Orderinfo>
    {
        List<Orderinfo> GetOrderList(Expression<Func<Orderinfo, bool>> where);
        Orderinfo GetOrderInfo(int orderId);       
    }
}
