using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.Model.Data;
using System.ServiceModel;
using System.ServiceModel.Web;
using WCF.Model.Service.ouput;


namespace WCF.Service.Interface
{
    [ServiceContract]
    public interface IOrderInfo_Service
    {
        [OperationContract]
        List<Orderinfo> GetOrderList();
    }
}
