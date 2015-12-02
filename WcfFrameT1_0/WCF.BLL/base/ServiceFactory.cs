using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.IBLL;

namespace WCF.BLL
{
    public class ServiceFactory
    {
        public static IAreaDataService AreaDataService { get { return new AreaDataService(); } }
        public static IOrderInfoDataService OrderInfoDataService { get { return new OrderInfoDataService(); } }
    }
}
