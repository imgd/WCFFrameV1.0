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
    [OperationInterceptorClass]
    public class AreaData_Service : IAreaData_Service
    {             
        public List<AreaData> GetParentChildAreaData()
        {
            return ServiceFactory.AreaDataService.GetTopAreaData(0);            
        }

        public List<AreaData> GetPagerAreaData(int pageSize, int pageIndex,out int dataCount)
        {
            return ServiceFactory.AreaDataService.GetPagerAreaData(pageSize,pageIndex,out dataCount);   
        }
    }
}
