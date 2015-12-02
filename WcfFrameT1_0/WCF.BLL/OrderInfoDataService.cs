using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.DAL;
using WCF.IBLL;
using WCF.IDAL;
using WCF.Model.Data;

namespace WCF.BLL
{
    public class OrderInfoDataService : BaseService<Orderinfo>, IOrderInfoDataService
    {

        private IOrderInfoRespository _orderinfo;
        public override void SetCurrentRepository()
        {
            _orderinfo = RepositoryFactory.OrderInfoRepository;
        }

        public List<Orderinfo> GetOrderList(System.Linq.Expressions.Expression<Func<Orderinfo, bool>> where)
        {
            var orderContext = _orderinfo.curDbContext.Set<Orderinfo>();
            var orderList = orderContext.Where(where).OrderByDescending(o => o.Id).ToList();
            return orderList;
        }




        public Orderinfo GetOrderInfo(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
