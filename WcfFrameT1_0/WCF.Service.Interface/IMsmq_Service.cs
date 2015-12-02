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
    public interface IMsmq_Service
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "InsertOrder",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        Response<string> InsertOrder();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CreateQueueOrder",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
       Response<string> CreateQueueOrder();
    }
}
