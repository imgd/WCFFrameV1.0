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
    public interface IAreaData_Service
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetParentChildAreaData",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<AreaData> GetParentChildAreaData();


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetPagerAreaData",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<AreaData> GetPagerAreaData(int pageSize, int pageIndex, out int dataCount);
    }
}
