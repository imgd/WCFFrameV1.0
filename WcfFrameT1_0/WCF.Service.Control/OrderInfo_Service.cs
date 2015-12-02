using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF.BLL;
using WCF.Inspector;
using WCF.Model.Data;
using WCF.Model.Service.ouput;
using WCF.Service.Interface;

namespace WCF.Service.Control
{
    public class OrderInfo_Service : IOrderInfo_Service
    {

        public List<Orderinfo> GetOrderList()
        {
            return ServiceFactory.OrderInfoDataService.GetOrderList(o => o.Id < 10); 
        }
    }
}
